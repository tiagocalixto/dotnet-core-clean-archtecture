using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyLittlePetShop.DataProvider.context;
using MyLittlePetShop.DataProvider.mapper;
using MyLittlePetShop.DataProvider.models;
using MyLittlePetShop.Entity.entities;
using MyLittlePetShop.Entity.repositories;


namespace MyLittlePetShop.DataProvider.repositories
{

    public class OwnerRepository : IOwnerRepository
    {
        private readonly PostgreSqlContext _context;
        
        public OwnerRepository(PostgreSqlContext context)
        {
            this._context = context;
        }

        public void DeleteById(int ownerId)
        {
            var item = GetOwnerdbEagger(ownerId);

            this._context.Owner.Remove(item);
            this._context.SaveChanges();
        }

        public bool ExistsById(int id)
        {
            var db = this._context.Owner
                        .Where(p => p.OwnerId == id)
                        .Select(p => p.OwnerId);

            return db != null;
        }

        public Owner FindByContact(string contactType, string contactValue)
        {
            
            ContactDb db = this._context.Contacts
                            .FirstOrDefault(a => a.Value.ToUpper() == contactValue.ToUpper() &&
                                a.Type.ToUpper() == contactType.ToUpper());
   
            return OwnerDbMapper.ConvertDbToEntity(db?.Owner);
        }

        public Owner FindById(int id)
        {
            return OwnerDbMapper.ConvertDbToEntity(this._context.Owner.Find(id));
        }

        public List<Owner> FindByName(string name, int pageSize, int page)
        {
             var db = this._context.Owner
                 .Where(x => x.Name.ToUpper().Contains(name.ToUpper()))
                 .OrderBy(x => x.Name)
                 .Skip((page - 1) * pageSize)
                 .Take(pageSize)
             .ToList();  

            return OwnerDbMapper.ConvertDbToEntity(db);
        }
 
        public Owner FindByPetChip(string chipId)
        {

            PetDb db = this._context.Pets
                            .FirstOrDefault(a => a.ChipId.ToUpper() == chipId.ToUpper());

            return OwnerDbMapper.ConvertDbToEntity(db?.Owner);

        }

        public Owner Save(Owner owner)
        {
            OwnerDb db = PrepareToSave(owner);

            this._context.Owner.Add(db);
            this._context.SaveChanges();

            return OwnerDbMapper.ConvertDbToEntity(db);
        }

        public Owner Update(Owner owner)
        {

            OwnerDb db = PrepareToUpdate(owner);

            this._context.Owner.Update(db);
            this._context.SaveChanges();

            PostUpdate(ref db);

            return OwnerDbMapper.ConvertDbToEntity(db);
        }

        private OwnerDb GetOwnerdbEagger(int id)
        {
            return _context.Owner
                   .Include(i => i.Pets)
                   .Include(i => i.Contacts)
                   .FirstOrDefault(x => x.OwnerId == id);
        }

        private OwnerDb PrepareToSave(Owner owner)
        {
            owner.Id = 0;
            owner.Contacts.Select(i => { i.Id = 0; return i; }).ToList();
            owner.Pets.Select(i => { i.Id = 0; return i; }).ToList();

            return OwnerDbMapper.ConvertEntityToDb(owner);
        }

        private OwnerDb PrepareToUpdate(Owner owner)
        {
            OwnerDb db = OwnerDbMapper.ConvertEntityToDb(owner);
            OwnerDb old = GetOwnerdbEagger(owner.Id);

            DetacheBeforeUpdate(old);
            PrepareChieldsObjectsToDelete(ref db, old);
            PrepareChildObjectsId(ref db, old);

            return db;
        }

        private void DetacheBeforeUpdate(OwnerDb owner)
        {
            _context.Entry(owner).State = EntityState.Detached;
        }

        private void PrepareChieldsObjectsToDelete(ref OwnerDb owner,
                                                   OwnerDb oldOwner)
        {
            List<ContactDb> contactToDelete = MarkSoftDeleteOnDeletedContacts(owner, oldOwner.Contacts);
            List<PetDb> petToDelete = MarkSoftDeleteOnDeletedPet(owner, oldOwner.Pets);

            foreach (var item in contactToDelete)
            {
                item.Owner = owner;
                item.IsDeleted = true;
                owner.Contacts.Add(item);
            }

            foreach (var item in petToDelete)
            {
                item.Owner = owner;
                item.IsDeleted = true;
                owner.Pets.Add(item);
            }
        }

        private List<ContactDb> MarkSoftDeleteOnDeletedContacts(OwnerDb owner,
                                                     IList<ContactDb> oldContacts)
        {
            List <ContactDb> toDelete = oldContacts.Where(p =>
                            !owner.Contacts.Any(p2 => p2.ContactId == p.ContactId)).ToList();

            toDelete
                .Where(i => i != null)
                .Select(i => {i.IsDeleted = true; return i; })
                .ToList();

            return toDelete;
        }

        private List<PetDb> MarkSoftDeleteOnDeletedPet(OwnerDb owner,
                                             IList<PetDb> oldPets)
        {
            List<PetDb> toDelete = oldPets.Where(p =>
                           !owner.Pets.Any(p2 => p2.PetId == p.PetId)).ToList();

            toDelete
                .Where(i => i != null)
                .Select(i => { i.IsDeleted = true; return i; })
                .ToList();

            return toDelete;
        }

        private void PrepareChildObjectsId(ref OwnerDb owner,
                                                   OwnerDb oldOwner)
        {
            owner.Contacts = SetZeroToInexistingIdContacts(owner.Contacts, oldOwner.Contacts);
            owner.Pets = SetZeroToInexistingIdPets(owner.Pets, oldOwner.Pets);
        }

        private IList<ContactDb> SetZeroToInexistingIdContacts(IList<ContactDb> contacts,
                                     IList<ContactDb> oldContacts)
        {
            contacts
                 .Where(p =>
                           !oldContacts.Any(p2 => p2.ContactId == p.ContactId))
                .Select(i => { i.ContactId = 0; return i; })
                .ToList();

            return contacts;
        }

        private IList<PetDb> SetZeroToInexistingIdPets(IList<PetDb> pets,
                                             IList<PetDb> oldPets)
        {
            pets
                 .Where(p =>
                           !oldPets.Any(p2 => p2.PetId == p.PetId))
                .Select(i => { i.PetId = 0; return i; })
                .ToList();

            return pets;
        }

        private void PostUpdate(ref OwnerDb owner)
        {
            owner.Contacts = RemoveDeletedContactsOnUpdate(owner.Contacts);
            owner.Pets = RemoveDeletePetsdOnUpdate(owner.Pets);
        }

            private IList<ContactDb> RemoveDeletedContactsOnUpdate(IList<ContactDb> contacts)
        {
            return contacts
                    .Where(i => !i.IsDeleted)
                    .ToList();
        }

        private IList<PetDb> RemoveDeletePetsdOnUpdate(IList<PetDb> pets)
        {
            return pets
                    .Where(i => !i.IsDeleted)
                    .ToList();
        }
    }
}