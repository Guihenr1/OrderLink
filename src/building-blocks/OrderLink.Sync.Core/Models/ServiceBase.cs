using OrderLink.Sync.Core.Notifications;
using FluentValidation;
using FluentValidation.Results;
using prmToolkit.NotificationPattern;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Notification = OrderLink.Sync.Core.Notifications.Notification;

namespace OrderLink.Sync.Core.Models
{
    public class ServiceBase : Notifiable, IServiceBase
    {
        private readonly INotificator _notificador;

        public ServiceBase(INotificator notificador)
        {
            _notificador = notificador;
        }

        protected void NotifyByEmail(string message)
        {
            AddNotification(null, message);
            Notify(message);
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage);
            }
        }

        protected void Notify(string mensagem)
        {
            _notificador.Handle(new Notification(mensagem));
        }

        protected bool ExecuteValidation<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : EntityBase
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            Notify(validator);

            return false;
        }

        protected static StringContent GetContent(object dado)
        {
            return new StringContent(JsonSerializer.Serialize(dado), Encoding.UTF8, "application/json");
        }

        protected static async Task<T> DeserializarObjetoResponse<T>(HttpResponseMessage responseMessage)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = false,
                    Converters = { new DateTimeOffsetConverter() }
                };

                return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
            } catch (Exception ex)
            {
                throw new Exception($"Error on Deserialize, JSON:{await responseMessage.Content.ReadAsStringAsync()};", ex);
            }
        }

        public static string SerializeObject<T>(T obj)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false,
                WriteIndented = true
            };

            return JsonSerializer.Serialize(obj, options);
        }

        protected bool HasNotifications() => _notificador.HasNotification();

        protected List<string> GetNotifications()
        {
            return _notificador.GetNotifications().Select(s => s.Mensagem).ToList();
        }
        protected void CleanNotifications()
        {
            _notificador.CleanNotifications();
        }
        private class DateTimeOffsetConverter : JsonConverter<DateTimeOffset>
        {
            public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return DateTimeOffset.Parse(reader.GetString());
            }

            public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString());
            }
        }
    }
}
