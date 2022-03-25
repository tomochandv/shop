using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace shop_admin_api.Lib
{
	public class Ses
	{
		private readonly IConfiguration _configuration;
		public Ses(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		/// <summary>
		/// 메일 보내기
		/// </summary>
		/// <param name="receiverAddress">받는 사람</param>
		/// <param name="subject">제목</param>
		/// <param name="htmlBody">내용</param>
		public void SendEmail(string receiverAddress = "", string subject = "", string htmlBody = "")
		{
			var senderAddress = _configuration["Aws:email"];
			using (var client = new AmazonSimpleEmailServiceClient(_configuration["Aws:accessKey"], _configuration["Aws:secretKey"], RegionEndpoint.APNortheast2))
			{
				var sendRequest = new SendEmailRequest
				{
					Source = senderAddress,
					Destination = new Destination
					{
						ToAddresses =
							new List<string> { receiverAddress }
					},
					Message = new Message
					{
						Subject = new Content(subject),
						Body = new Body
						{
							Html = new Content
							{
								Charset = "UTF-8",
								Data = htmlBody
							}
						}
					}
				};
				try
				{
					var response = client.SendEmailAsync(sendRequest);
				}
				catch (Exception)
				{
					throw;
				}
			}
		}
	}
}
