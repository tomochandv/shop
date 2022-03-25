using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using shop_admin_api.Model.S3;
using shop_model;
using System;
using System.Threading.Tasks;

namespace shop_admin_api.Lib
{
	public class S3
	{
		private readonly IConfiguration _configuration;
		private string bucketName;
		private AmazonS3Client s3Client;

		public S3(IConfiguration configuration)
		{
			_configuration = configuration;
			bucketName = _configuration["Aws:bucket"];
			s3Client = new AmazonS3Client(_configuration["Aws:accessKey"], _configuration["Aws:secretKey"], RegionEndpoint.APNortheast2);
		}

		public async Task<S3Result> Upload(IFormFile file, FileTpye type)
		{
			try
			{
				var s3result = new S3Result();
				var path = type.ToString();
				var filePath = $"{bucketName}/shop/{path}/{DateTime.Now.ToString("yyyyMMdd")}";
				var filename = DateTime.Now.ToString("yyyyMMddhhmmssff");
				var extension = file.FileName.Split(".")[file.FileName.Split(".").Length - 1];
				filename = $"{filename}.{extension}";

				var fileTransferUtility = new TransferUtility(s3Client);
				var fileTransferUtilityRequest = new TransferUtilityUploadRequest
				{
					InputStream = file.OpenReadStream(),
					BucketName = "/" + filePath,
					Key = filename,
					CannedACL = S3CannedACL.PublicRead
				};
				await fileTransferUtility.UploadAsync(fileTransferUtilityRequest);
				s3result.bucket = filePath;
				s3result.filename = filename;
				return s3result;
			}
			catch (Exception)
			{
				throw;
			}
		}

		//public async Task<S3Result> Upload(IFormFile file, FileTpye fileType, int productidx)
		//{
		//	try
		//	{
		//		var s3result = new S3Result();
		//		var path = string.Empty;
		//		if (fileType == FileTpye.ListImage)
		//		{
		//			path = "list";
		//		}
		//		else if (fileType == FileTpye.DetailImage)
		//		{
		//			path = "detail";
		//		}
		//		var filePath = $"{bucketName}/shop/{path}/{productidx}";
		//		var filename = DateTime.Now.ToString("yyyyMMddhhmmssff");
		//		var extension = file.FileName.Split(".")[file.FileName.Split(".").Length - 1];
		//		filename = $"{filename}.{extension}";

		//		var fileTransferUtility =  new TransferUtility(s3Client);
		//		var fileTransferUtilityRequest = new TransferUtilityUploadRequest
		//		{
		//			InputStream = file.OpenReadStream(),
		//			BucketName = "/" + filePath,
		//			Key = filename,
		//			CannedACL = S3CannedACL.PublicRead
		//		};
		//		await fileTransferUtility.UploadAsync(fileTransferUtilityRequest);
		//		s3result.bucket = filePath;
		//		s3result.filename = filename;
		//		return s3result;
		//	}
		//	catch(Exception)
		//	{ 
		//		throw; 
		//	}
		//}
	}
}
