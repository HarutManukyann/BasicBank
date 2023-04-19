using BL.Hashing;
using BL.Repositories;
using FCBankBasicHelper.Models;
using Models.BaseType;
using Models.DTO;
using Models.Mappers;


namespace BL.Core
{
    public class PhoneBl
    {   
        private readonly PhoneRepository phoneRepository;
        private readonly Validation validation;
        private readonly Encryption encryption;
        public PhoneBl(Encryption encryption ,Validation validation, PhoneRepository phoneRepository)
        {
            this.phoneRepository = phoneRepository;
            this.validation = validation;
            this.encryption = encryption;
        }
        public ResponseBase<PhoneModel> GetPhone(int userId)
        {
            try
            {
                Phone phone = phoneRepository.GetPhone(userId);
                if (phone == null) throw new Exception("Phone not found");
                encryption.DecryptData(phone);
                PhoneModel model = Mapper<Phone, PhoneModel>.Map(phone);
                return new ResponseBase<PhoneModel>(true, "User Phone", model);
            }
            catch (Exception ex)
            {
                return new ResponseBase<PhoneModel>(false, ex.Message);
            }
        }
        public ResponseBase<PhoneModel> Insertphone(PhoneModel model)
        {
                try
                {
                    if (!validation.PhoneValidation(model.PhoneNumber)) throw new Exception("Invalid phone");
                    Phone phone = Mapper<PhoneModel, Phone>.Map(model);
                    encryption.EncryptData(phone);
                    phoneRepository.Add(phone);
                    return new ResponseBase<PhoneModel>(true, "Phone added successfully", model);
                }
                catch (Exception ex)
                {
                    return new ResponseBase<PhoneModel>(false, ex.Message);
                }
        }
        public ResponseBase<PhoneModel> Updatephone(PhoneModel model)
        {
                try
                {
                    if (!validation.PhoneValidation(model.PhoneNumber)) throw new Exception("Invalid phone");
                    Phone phone = Mapper<PhoneModel, Phone>.Map(model);
                    encryption.EncryptData(phone);
                    phoneRepository.Update(phone);
                    return new ResponseBase<PhoneModel>(true, "Phone updated successfully", model);
                }
                catch (Exception ex)
                {
                    return new ResponseBase<PhoneModel>(false, ex.Message);
                }
         }
        public ResponseBase<PhoneModel> Deletephone(PhoneModel model)
        {
                try
                {
                    Phone phone = Mapper<PhoneModel, Phone>.Map(model);
                    encryption.EncryptData(phone);
                    phoneRepository.Remove(phone);
                    return new ResponseBase<PhoneModel>(true, "Phone deleted successfully", model);
                }
                catch (Exception ex)
                {
                    return new ResponseBase<PhoneModel>(false, ex.Message);
                }
        }
    }
}
