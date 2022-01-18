using System;
using System.Collections.Generic;
using MyLittlePetShop.Entity.entities;
using MyLittlePetShop.UseCase.handler.interfaces;
using MyLittlePetShop.UseCase.owner.delete.interfaces;
using MyLittlePetShop.UseCase.owner.find.interfaces;
using MyLittlePetShop.UseCase.owner.persist.interfaces;

namespace MyLittlePetShop.UseCase.handler
{
    public class UseCaseHandler : IUseCaseHandler
    {
        #region "properties"
        private IDeleteOwnerUseCase _deleteOwner;
        private IFindOwnerByContactUseCase _findOwnerByContact;
        private IFindOwnerByIdUseCase _findOwnerById;
        private IFindOwnerByNameUseCase _findOwnerByName;
        private ISaveOwnerUseCase _saveOwner;
        private IUpdateOwnerUseCase _updateOwner;
        #endregion

        #region "constructor"
        public UseCaseHandler(IDeleteOwnerUseCase deleteOwner,
                              IFindOwnerByContactUseCase findOwnerByContact,
                              IFindOwnerByIdUseCase findOwnerById,
                              IFindOwnerByNameUseCase findOwnerByName,
                              ISaveOwnerUseCase saveOwner,
                              IUpdateOwnerUseCase updateOwner)
        {
            this._deleteOwner = deleteOwner;
            this._findOwnerByContact = findOwnerByContact;
            this._findOwnerById = findOwnerById;
            this._findOwnerByName = findOwnerByName;
            this._saveOwner = saveOwner;
            this._updateOwner = updateOwner;
        }
        #endregion

        public void DeleteOwnerById(int id)
        {
            _deleteOwner.Execute(id);
        }

        public List<Owner> FindOwnerByName(string name, int pageSize, int page)
        {
            return _findOwnerByName.Execute(name, pageSize, page);
        }

        public Owner FindOwnerByContact(string contactType, string contactValue)
        {
            return _findOwnerByContact.Execute(contactType, contactValue);
        }

        public Owner FindOwnerById(int id)
        {
            return _findOwnerById.Execute(id);
        }

        public Owner SaveOwner(Owner owner)
        {
            return _saveOwner.Execute(owner);
        }

        public Owner UpdateOwner(Owner owner)
        {
            return _updateOwner.Execute(owner);
        }
    }
}
