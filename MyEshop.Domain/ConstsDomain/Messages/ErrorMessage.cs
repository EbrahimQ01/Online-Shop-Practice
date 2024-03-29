﻿
namespace MyEshop.Domain.ConstsDomain.Messages
{
    public static class ErrorMessage
    {
        public const string Required = " لطفا " + "{0}" + " پر کنید ";
        public const string MaxLength = "{0}" + " باید کمتر از " + "{1}" + " کارکتر داشته باشد ";
        public const string MinLength = "{0}" + " باید بیشرتر از " + "{1}" + " کارکتر داشته باشد ";
        public const string StringLength = "{0}" + " باید کمتر از " + "{1}" + " و بیشتر از " + "{2}" + " کارکتر باشد";
        public const string SuccessFormat = "{0} مورد تایید نمی باشد";
        public const string IsExist = "{0} وارد شده از قبل موجود است";
        public const string RangeNumber = "{0} وارد شده باید بیشتر از {1} و کمتر از {2} باشد";
        public const string Compare = "{0} باید با {1} مطابقت داشته باشد";
        public const string ExceptionSave = "مشکلی در ذخیره وجود دارد";
        public const string ExceptionFileSave = "مشکلی در ذخیره فایل وجود دارد";
        public const string ExceptionExistCategory = "گروه وارد شده مورد تایید نمی باشد";
        public const string ExceptionExistTags = "برچسب های وارد شده مورد تایید نمی باشد";
        public const string ExceptionCommentsDelete = "مشکلی در حذف کامنت های این محصول به وجود آمده";
        public const string ExceptionImagesFind = "مشکلی در یافتن تصاویر این محصول به وجود آمده";
        public const string ExceptionImagesDeletse = "مشکلی در حذف تصاویر این محصول به وجود آمده";
        public const string ExceptionProductDelete = "مشکلی در حذف این محصول به وجود آمده";
        public const string ExceptionFileImagesDelete = "مشکلی در حذف فایل های تصاویر این محصول به وجود آمده";
        public const string ExceptionFileImagesType = "نوع فایل ارسالی قابل قبول نمی باشد";
        public const string ExceptionAvailableImages = "تصاویر انتخاب شده برای حذف قابل قبول نمی باشد";
        public const string ExceptionTagsDelete = "مشکلی در حذف این برچسب ها به وجود آمده";

        public static string ExceptionEdit(string name) => $"در ویرایش {name} مشکلی به وجود آمده";
        public static string ExceptionDelete(string name) => $"در حذف {name} مشکلی به وجود آمده";
        public static string ExceptionCreate(string name) => $"در ساخت {name} مشکلی به وجود آمده";
        public static string NotFound(string name) => $"{name} پیدا نشد";
        public static string IsExistWithName(string name) => IsExist.Replace("{0}", name);

    }
}