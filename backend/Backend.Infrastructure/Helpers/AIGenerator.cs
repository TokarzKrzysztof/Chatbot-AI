using Backend.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Helpers
{
    public static class AIGenerator
    {
        private static readonly string[] _responses = [
            "Lorem ipsum dolor sit amet consectetur adipisicing elit.",
            "Lorem ipsum dolor sit amet consectetur adipisicing elit. Sed, cumque natus esse accusamus soluta harum magni quam? Sapiente aliquid illum, excepturi maxime tenetur, quas, fugiat inventore vero sequi repudiandae et?",
            "Lorem ipsum dolor sit amet consectetur adipisicing elit.\n Sed, cumque natus esse accusamus soluta harum magni quam? Sapiente aliquid illum. Excepturi maxime tenetur, quas, fugiat inventore vero sequi repudiandae et?",
            "Lorem ipsum dolor sit amet consectetur adipisicing elit.\n Sed, cumque natus esse accusamus soluta harum magni quam? Sapiente aliquid illum.\n Excepturi maxime tenetur, quas, fugiat inventore vero sequi repudiandae et?",
            "Lorem ipsum dolor sit amet consectetur adipisicing elit.\n Sed, cumque natus esse accusamus soluta harum magni quam? Sapiente aliquid illum.\n Excepturi maxime tenetur, quas, fugiat inventore vero sequi repudiandae et?\n Lorem ipsum dolor sit amet consectetur adipisicing elit. \n Sed, cumque natus esse accusamus soluta harum magni quam? Sapiente aliquid illum.\n Excepturi maxime tenetur, quas, fugiat inventore vero sequi repudiandae et?",
            "Lorem ipsum dolor sit amet consectetur adipisicing elit.\n Sed, cumque natus esse accusamus soluta harum magni quam? Sapiente aliquid illum.\n Excepturi maxime tenetur, quas, fugiat inventore vero sequi repudiandae et?\n Lorem ipsum dolor sit amet consectetur adipisicing elit. \n Sed, cumque natus esse accusamus soluta harum magni quam? Sapiente aliquid illum.\n Excepturi maxime tenetur, quas, fugiat inventore vero sequi repudiandae et? Lorem ipsum dolor sit amet consectetur adipisicing elit.\n Sed, cumque natus esse accusamus soluta harum magni quam? Sapiente aliquid illum.\n Excepturi maxime tenetur, quas, fugiat inventore vero sequi repudiandae et?\n Lorem ipsum dolor sit amet consectetur adipisicing elit. \n Sed, cumque natus esse accusamus soluta harum magni quam? Sapiente aliquid illum.\n Excepturi maxime tenetur, quas, fugiat inventore vero sequi repudiandae et? Lorem ipsum dolor sit amet consectetur adipisicing elit.\n Sed, cumque natus esse accusamus soluta harum magni quam? Sapiente aliquid illum.\n Excepturi maxime tenetur, quas, fugiat inventore vero sequi repudiandae et?\n Lorem ipsum dolor sit amet consectetur adipisicing elit. \n Sed, cumque natus esse accusamus soluta harum magni quam? Sapiente aliquid illum.\n Excepturi maxime tenetur, quas, fugiat inventore vero sequi repudiandae et? Lorem ipsum dolor sit amet consectetur adipisicing elit.\n Sed, cumque natus esse accusamus soluta harum magni quam? Sapiente aliquid illum.\n Excepturi maxime tenetur, quas, fugiat inventore vero sequi repudiandae et?\n Lorem ipsum dolor sit amet consectetur adipisicing elit. \n Sed, cumque natus esse accusamus soluta harum magni quam? Sapiente aliquid illum.\n Excepturi maxime tenetur, quas, fugiat inventore vero sequi repudiandae et? Lorem ipsum dolor sit amet consectetur adipisicing elit.\n Sed, cumque natus esse accusamus soluta harum magni quam? Sapiente aliquid illum.\n Excepturi maxime tenetur, quas, fugiat inventore vero sequi repudiandae et?\n Lorem ipsum dolor sit amet consectetur adipisicing elit. \n Sed, cumque natus esse accusamus soluta harum magni quam? Sapiente aliquid illum.\n Excepturi maxime tenetur, quas, fugiat inventore vero sequi repudiandae et?",
        ];

        public static async IAsyncEnumerable<string> Generate(CancellationToken cancellationToken)
        {
            string chatResponse = _responses.Random();

            StringBuilder sb = new();
            int i = 0;
            while (!cancellationToken.IsCancellationRequested)
            {
                if (i == chatResponse.Length) break;

                sb.Append(chatResponse[i]);
                yield return sb.ToString();

                await Task.Delay(10);

                i++;
            }

        }
    }
}
