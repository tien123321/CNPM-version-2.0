using CNPM.Model.ClassData;
using CNPM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM.Controler
{
    internal class NXBControler
    {
        private string username, address, phone;
        private NXBModel _NXBModel;
        public NXBControler()
        {
            this._NXBModel = new NXBModel();
        }
        public List<NXB> searchNXBs(NXB filterNXB)
        {
            return this._NXBModel.find(filterNXB);
        }
        public List<NXB> allNXBs()
        {
            return this._NXBModel.findAll();
        }
        public bool isExistNXB(NXB NXB)
        {
            return this._NXBModel.exist(NXB);
        }
        public NXB createNXB(NXB NXB)
        {
            bool isValid = NXB.requiredFields();
            if (isValid)
            {
                return this._NXBModel.create(NXB);
            }
            return new NXB(1, "", "", "");
        }
        public NXB updateNXB(NXB NXB)
        {
            bool isValid = NXB.requiredFields();
            if (isValid)
            {
                return this._NXBModel.update(NXB);
            }
            return new NXB(1, "", "", "");
        }
        public NXB deleteNXB(NXB NXB)
        {
            return this._NXBModel.delete(NXB);
        }
    }
}
