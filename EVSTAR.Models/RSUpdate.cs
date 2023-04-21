using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVSTAR.Models
{
     public class Attributes
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("customer_id")]
        public int CustomerId { get; set; }

        [JsonProperty("customer_business_then_name")]
        public string CustomerBusinessThenName { get; set; }

        [JsonProperty("due_date")]
        public DateTime DueDate { get; set; }

        [JsonProperty("start_at")]
        public object StartAt { get; set; }

        [JsonProperty("end_at")]
        public object EndAt { get; set; }

        [JsonProperty("location_id")]
        public object LocationId { get; set; }

        [JsonProperty("problem_type")]
        public string ProblemType { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("properties")]
        public Properties Properties { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("pdf_url")]
        public object PdfUrl { get; set; }

        [JsonProperty("intake_form_html")]
        public object IntakeFormHtml { get; set; }

        [JsonProperty("signature_name")]
        public object SignatureName { get; set; }

        [JsonProperty("signature_date")]
        public object SignatureDate { get; set; }

        [JsonProperty("asset_ids")]
        public List<int> AssetIds { get; set; }

        [JsonProperty("priority")]
        public object Priority { get; set; }

        [JsonProperty("resolved_at")]
        public object ResolvedAt { get; set; }

        [JsonProperty("outtake_form_name")]
        public object OuttakeFormName { get; set; }

        [JsonProperty("outtake_form_date")]
        public object OuttakeFormDate { get; set; }

        [JsonProperty("outtake_form_html")]
        public object OuttakeFormHtml { get; set; }

        [JsonProperty("address")]
        public object Address { get; set; }

        [JsonProperty("comments")]
        public List<Comment> Comments { get; set; }

        [JsonProperty("attachments")]
        public List<object> Attachments { get; set; }

        [JsonProperty("ticket_timers")]
        public List<object> TicketTimers { get; set; }

        [JsonProperty("line_items")]
        public List<LineItem> LineItems { get; set; }

        [JsonProperty("worksheet_results")]
        public List<WorksheetResult> WorksheetResults { get; set; }

        [JsonProperty("assets")]
        public List<Asset> Assets { get; set; }

        [JsonProperty("appointments")]
        public List<object> Appointments { get; set; }

        [JsonProperty("ticket_fields")]
        public List<TicketField> TicketFields { get; set; }

        [JsonProperty("ticket_answers")]
        public List<object> TicketAnswers { get; set; }

        [JsonProperty("customer")]
        public Customer Customer { get; set; }

        [JsonProperty("contact")]
        public Contact Contact { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("ticket_type")]
        public TicketType TicketType { get; set; }
    }

    public class Content
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("options")]
        public object Options { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("filled_by")]
        public string FilledBy { get; set; }
    }

    public class RSUpdate
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("html")]
        public string Html { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; }
    }

    public class TicketField
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("field_type")]
        public string FieldType { get; set; }

        [JsonProperty("required")]
        public bool Required { get; set; }

        [JsonProperty("account_id")]
        public int AccountId { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("ticket_type_id")]
        public int TicketTypeId { get; set; }

        [JsonProperty("hidden")]
        public bool Hidden { get; set; }

        [JsonProperty("position")]
        public object Position { get; set; }

        [JsonProperty("answers")]
        public List<object> Answers { get; set; }
    }

    public class TicketType
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("account_id")]
        public int AccountId { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("disabled")]
        public bool Disabled { get; set; }

        [JsonProperty("intake_terms")]
        public object IntakeTerms { get; set; }

        [JsonProperty("skip_intake")]
        public bool SkipIntake { get; set; }

        [JsonProperty("outtake_terms")]
        public object OuttakeTerms { get; set; }

        [JsonProperty("skip_outtake")]
        public bool SkipOuttake { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("ticket_fields")]
        public List<TicketField> TicketFields { get; set; }
    }

    public class WorksheetResult
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("worksheet_template_id")]
        public int WorksheetTemplateId { get; set; }

        [JsonProperty("ticket_id")]
        public int TicketId { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("content")]
        public List<Content> Content { get; set; }

        [JsonProperty("complete")]
        public bool Complete { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("public")]
        public bool Public { get; set; }

        [JsonProperty("required")]
        public bool Required { get; set; }
    }

}
