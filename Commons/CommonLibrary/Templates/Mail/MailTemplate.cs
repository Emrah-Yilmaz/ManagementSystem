namespace CommonLibrary.Templates.Mail
{
    public static class MailTemplate
    {
        public static readonly string CreatedDepartment = "<p>Merhaba,</p>" +
        "<p><strong>{0}</strong> tarihinde, <strong>{1}</strong> tarafından yeni bir departman oluşturulmuştur.</p>" +
        "<p><strong>Department bilgileri aşağıdaki gibidir.</strong></p>" +
        "<p>Departman Id: <strong>{2}</strong></p>" +
        "<p>Departman Adı: <strong>{3}</strong></p>" +
        "<p>Saygılarımızla,<br/>Ekibiniz</p>";
        public static readonly string Admin = "emrahyilmaz57@outlook.com.tr";

    }
}
