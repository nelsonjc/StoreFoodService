using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFood.Domain.Variables
{
    public static class Swagger
    {
        public static string SW_TITLE { get { return "Shop Food Service"; } }
        public static string SW_DESCRIPTION { get { return "Shop Food API REST to get and set information "; } }
        public static string SW_NAME { get { return "Nelson Jaramillo"; } }
        public static string SW_EMAIL { get { return "nelsonjaramilloc@gmail.com"; } }
        public static string SW_URL_JSON { get { return "./v1/swagger.json"; } }
        public static string SW_VERSION { get { return "v1"; } }
        public static string SW_SEC_NAME { get { return "Authorization"; } }
        public static string SW_SEC_DEF_DESCRIPTION { get { return "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below. Example: 'Bearer 1234xxxx'"; } }
        public static string SW_SEC_DEF_SCHEME { get { return "Bearer"; } }
        public static string SW_COMMENT_PATH_EXT { get { return ".xml"; } }
    }
}
