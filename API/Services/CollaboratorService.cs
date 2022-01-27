using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using API.Context;
using API.Models;
using API.Repository.Interfaces;
using API.Services.Interfaces;

namespace API.Services
{
    public class CollaboratorService: ICollaboratorService
    {
        private readonly ICollaboratorRepository _collaboratorRepository;

        public CollaboratorService(ICollaboratorRepository collaboratorRepository)
        {
            _collaboratorRepository = collaboratorRepository;
        }
        public IEnumerable<Collaborator> GetAllActive()
        {
            return _collaboratorRepository.GetAll().ToList().Where(x => x.isActive == true);
        }
        public IEnumerable<Collaborator> GetAllDisable()
        {
            return _collaboratorRepository.GetAll().ToList().Where(x => x.isActive == false);
        }
        public Collaborator GetByCpf(string cpf)
        {
            var coll = _collaboratorRepository.SearchCpf(cpf);
            return coll;
        }
        public Collaborator GetByName(string name)
        {
            var coll = _collaboratorRepository.SearchName(name);
            return coll;
        }
        public Collaborator Add(Collaborator collaborator)
        {
            return _collaboratorRepository.Add(collaborator);
        }
        public void Update(Collaborator collaborator)
        {
            _collaboratorRepository.Update(collaborator);
        }
        public Boolean Disable(string cpf)
        {
            var coll = _collaboratorRepository.SearchCpf(cpf);
            if (coll == null)
                return false;
            if (coll.isActive == false)
                return false;
            coll.isActive = false;
            _collaboratorRepository.Update(coll);
            return true;
        }
        public Boolean Reactivate(string cpf)
        {
            var coll = _collaboratorRepository.SearchCpf(cpf);
            if (coll == null)
                return false;
            if (coll.isActive == true)
                return false;
            coll.isActive = true;
            _collaboratorRepository.Update(coll);
            return true;
        }
    }
}