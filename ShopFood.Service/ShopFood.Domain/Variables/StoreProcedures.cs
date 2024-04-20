namespace ShopFood.Domain.Variables
{
    /// <summary>
    /// Class to get store procedure
    /// </summary>
    public static class StoreProcedures
    {
        public static string EXEC { get { return "EXEC"; } }

        #region FoodCatalog
        public static string FoodCatalog_GetBy_Id { get { return "[dbo].[SP_FoodCatalog_GetBy_Id]"; } }
        public static string FoodCatalog_GetAll { get { return "[dbo].[SP_FoodCatalog_GetAll]"; } }
        public static string FoodCatalog_Insert { get { return "[dbo].[SP_FoodCatalog_Insert]"; } }
        public static string FoodCatalog_Update { get { return "[dbo].[SP_FoodCatalog_Update]"; } }
        public static string FoodCatalog_Delete { get { return "[dbo].[SP_FoodCatalog_Delete]"; } }
        #endregion

        #region FoodOrder
        public static string FoodOrder_Confirm { get { return "[dbo].[SP_FoodOrder_Confirm]"; } }
        public static string FoodOrder_Insert { get { return "[dbo].[SP_FoodOrder_Insert]"; } }
        public static string SP_FoodOrder_GetAll { get { return "[dbo].[SP_FoodOrder_GetAll]"; } }
        public static string SP_FoodOrder_GetBy_Id { get { return "[dbo].[SP_FoodOrder_GetBy_Id]"; } }
        
        #endregion

        #region User
        public static string User_Delete { get { return "[dbo].[SP_User_Delete]"; } }
        public static string User_GetAll { get { return "[dbo].[SP_User_GetAll]"; } }
        public static string User_GetBy_Id { get { return "[dbo].[SP_User_GetBy_Id]"; } }
        public static string User_GetBy_UserName { get { return "[dbo].[SP_User_GetBy_UserName]"; } }
        public static string User_Insert { get { return "[dbo].[SP_User_Insert]"; } }
        public static string User_CustomerInsert { get { return "[dbo].[SP_User_CustomerInsert]"; } }
        public static string User_Update { get { return "[dbo].[SP_User_Update]"; } } 
        #endregion
    }
}
