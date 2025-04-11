﻿namespace Storeify.Web.Const
{
    public static class Errors
    {
        public const string MaxLength = "Length cannot be more than {1} characters";
        public const string Duplicated = "{0} with the same name is already exists!";
        public const string DuplicatedBranch = "Branch with the same Name is already exists with the same Store!";
        public const string DuplicatedInventory = "Inventory with the same Name is already exists with the same Branch!";
        public const string NotAllowedExtension = "Only .png, .jpg, .jpeg files are allowed!";
        public const string MaxSize = "File cannot be more that 2 MB!";
        public const string NotAllowFutureDates = "Date cannot be in the future!";
        public const string PhoneNumber = "Phone number must be in the format (123) 456-78901";
    }
}
