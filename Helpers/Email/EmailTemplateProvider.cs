namespace WebAppDemo.Helpers.Email
{
    public class EmailTemplateProvider
    {
        /// <summary>
        /// This method can be used to get the email template to send refer to branch internal email
        /// </summary>
        /// <param name="studentName"></param>
        /// <returns></returns>
        public static string SendEmailOnSubjectCreated( string studentName)
        {
            var webRoot = AppDomain.CurrentDomain.BaseDirectory;
            var fullPath = Path.Combine(webRoot, "Assets/EmailTemplates/OnSubjectCreated.html");
       

            // read html template as a string
            var stringTemplate = SupportUtils.LoadStringFromFile(fullPath);

            // replace variable with dynamic values
            stringTemplate = stringTemplate.Replace("--STUDENT_NAME--", studentName);

            return stringTemplate;
        }
    }
}
