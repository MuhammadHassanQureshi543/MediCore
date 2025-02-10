using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data_TransferModels.ResponseModels
{
    public class ResponseVM
    {
        private static ResponseVM instance = null;

        public ResponseVM() { }

        public static ResponseVM Instance
        {
            get
            {
                {
                    instance = new ResponseVM();
                }
                return instance;
            }
        }
        public int responseCode { get; set; }
        public string errorMessage { get; set; } = "";
        public string responseMessage { get; set; } = "";
        public dynamic data { get; set; }

        public enum ResponseCode
        {
            Success,
            BadRequest,
            InternalServerError
        }
    }
}
