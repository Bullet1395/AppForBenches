using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppForBenches.Models
{
    /// <summary>
    /// Сопоставление названий, для отображения в таблицах/графиках
    /// </summary>    
    public class NameEquals
    {
        /// <summary>
        /// Id сопоставления
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }
        /// <summary>
        /// Заменяемое название
        /// </summary>
        [JsonPropertyName("old_name")]
        public string OldName { get; set; }

        /// <summary>
        /// Отображаемое название
        /// </summary>
        [JsonPropertyName("new_name")]
        public string NewName { get; set; }
    }
}
