using System.ComponentModel;

namespace OcelotGateWay.Enums.Common
{
    public enum DomainStatusCode
    {
        [Description("خطای کلاینت")]
        Fail, // Client did wrong
        [Description("خطای سرور")]
        Error, // Server did wrong
        [Description("هشدار")]
        Warning,
        [Description("عملیات با موفقیت انجام شد")]
        Success = 200, // Everything OK
        [Description("رکوردی یافت نشد")]
        NotFound = 404, // Everything OK
        [Description("بیش از حد مجاز")]
        LimitReached = 429,
        [Description("نیاز به پرداخت")]
        PaymentRequired = 402,
        [Description("توکن معتبر نیست")]
        InvalidToken = 498,
        [Description("نیاز به توکن")]
        TokenRequired = 499,
        [Description("مقادیر قابل قبول نیست")]
        NotAcceptable = 406,
        [Description("امضا وارد نشده است")]
        SignNotSet = 450,
        [Description("امضا نامعتبر است")]
        InvalidSign = 451,
        [Description("شماره موبایل نامعتبر است")]
        InvalidMobileNumber = 452,
        [Description("کد فعال سازی نامعتبر است")]
        InvalidActivationCode = 453,
        [Description("پکیج یافت نشد")]
        PackageNotFound = 454,
        [Description("زمان نامعتبر است")]
        InvalidTime = 455,
        [Description("کد قبلا ارسال شده است")]
        CodeSentBefore = 456,
        [Description("خطا در بانک اطلاعاتی")]
        DatabaseError = 457,
        [Description("خطای ناشناخته")]
        UnknownError = 500
    }
}
