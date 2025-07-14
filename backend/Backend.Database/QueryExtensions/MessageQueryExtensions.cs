using Backend.Models.Entities;
using DelegateDecompiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Database.QueryExtensions
{
	public static class MessageQueryExtensions
	{
		[Computed]
		public static MessageDTO AsDTO(this Message x)
		{
			return new MessageDTO()
			{
				Id = x.Id,
				CreatedAt = x.CreatedAt,
				Text = x.Text,
			};
		}
	}
}
