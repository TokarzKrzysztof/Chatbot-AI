using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Models.Entities
{
	public enum MessageReaction
	{
		None,
		Positive,
		Negative
	}

	public class Message
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public string Text { get; set; }
		public bool IsAnswer { get; set; }
        public MessageReaction Reaction { get; set; }
    }
	
	public class MessageDTO
	{
		public Guid Id { get; set; }
		public DateTime CreatedAt { get; set; }
		public string Text { get; set; }
		public bool IsAnswer { get; set; }
        public MessageReaction Reaction { get; set; }
    }
}
