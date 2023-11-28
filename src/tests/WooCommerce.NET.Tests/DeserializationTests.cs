using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using WooCommerceNET;
using WooCommerceNET.WooCommerce.v3;

namespace WooCommerce.NET.Tests
{
	[TestFixture]
	public class DeserializationTests
	{
		private readonly string _baseFilesPath;
		private readonly RestAPI _v3RestApi;

		public DeserializationTests()
		{
			_baseFilesPath = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)).LocalPath + "\\Files\\";
			_v3RestApi = new RestAPI("wp-json/wc/v3", "", "");
		}
	
		[TestCase(
			@"ProductsJsonResponse_WhenDatesAreIncorrect.json",
			"0001-01-01T00:00:00",
			TestName = "DeserializeJSon_ProductsHaveDateTimeMinValue_WhenDatesAreIncorrect")]
		[TestCase(
			@"ProductsJsonResponse_WhenDatesAreCorrect.json",
			"2023-11-29T19:03:58",
			TestName = "DeserializeJSon_ProductsHaveCorrectDateTimeValues_WhenDatesAreCorrect")]
		[TestCase(
			@"ProductsJsonResponse_WhenDatesAreNull.json",
			"0001-01-01T00:00:00",
			TestName = "DeserializeJSon_ProductsHaveDateTimeMinValue_WhenDatesAreNull")]
		[TestCase(
			@"ProductsJsonResponse_WhenDatesAreEmpty.json",
			"0001-01-01T00:00:00",
			TestName = "DeserializeJSon_ProductsHaveDateTimeMinValue_WhenDatesAreEmpty")]
		public void DateTimeDeserializeJson(string fileName, string expectedDateValue)
		{
			// arrange
			var jsonResponse = File.ReadAllText(_baseFilesPath + fileName);

			// act
			var products = _v3RestApi.DeserializeJSon<List<Product>>(jsonResponse);

			// assert
			var expectedDate = DateTime.Parse(expectedDateValue);
			Assert.That(products.Count, Is.EqualTo(1));
			Assert.That(products[0].date_created, Is.EqualTo(expectedDate));
			Assert.That(products[0].date_created_gmt, Is.EqualTo(expectedDate));
			Assert.That(products[0].date_modified, Is.EqualTo(expectedDate));
			Assert.That(products[0].date_modified_gmt, Is.EqualTo(expectedDate));
			Assert.That(products[0].date_on_sale_from, Is.EqualTo(expectedDate));
			Assert.That(products[0].date_on_sale_from_gmt, Is.EqualTo(expectedDate));
			Assert.That(products[0].date_on_sale_to, Is.EqualTo(expectedDate));
			Assert.That(products[0].date_on_sale_to_gmt, Is.EqualTo(expectedDate));
			Assert.That(products[0].images[0].date_created, Is.EqualTo(expectedDate));
			Assert.That(products[0].images[0].date_created_gmt, Is.EqualTo(expectedDate));
			Assert.That(products[0].images[0].date_modified, Is.EqualTo(expectedDate));
			Assert.That(products[0].images[0].date_modified_gmt, Is.EqualTo(expectedDate));
		}
		
		[TestCase(
			@"ProductsJsonResponse_WhenManageStockValueIsParent.json",
			false,
			TestName = "DeserializeJSon_ManageStockIsFalse_WhenManageStockValueIsParent")]
		[TestCase(
			@"ProductsJsonResponse_WhenManageStockValueIsTrue.json",
			true,
			TestName = "DeserializeJSon_ManageStockIsFalse_WhenManageStockValueIsTrue")]
		[TestCase(
			@"ProductsJsonResponse_WhenManageStockValueIsNull.json",
			false,
			TestName = "DeserializeJSon_ManageStockIsFalse_WhenManageStockValueIsNull")]
		[TestCase(
			@"ProductsJsonResponse_WhenManageStockValueIsFalse.json",
			false,
			TestName = "DeserializeJSon_ManageStockIsFalse_WhenManageStockValueIsFalse")]
		public void ManageStockDeserializeJson(string fileName, bool expectedManageStock)
		{
			// arrange
			var jsonResponse = File.ReadAllText(_baseFilesPath + fileName);

			// act
			var products = _v3RestApi.DeserializeJSon<List<Product>>(jsonResponse);

			// assert
			Assert.That(products.Count, Is.EqualTo(1));
			Assert.That(products[0].manage_stock, Is.EqualTo(expectedManageStock));
		}

		[TestCase(@"ProductsJsonResponse_WhenReviewsAllowedIsIncorrect.json")]
		public void DeserializeJSon_DoesNotThrowError_WhenReviewsAllowedIsIncorrect(string fileName)
		{
			// arrange
			var jsonResponse = File.ReadAllText(_baseFilesPath + fileName);

			// act / assert
			Assert.DoesNotThrow(() => _v3RestApi.DeserializeJSon<List<Product>>(jsonResponse));
		}
	}
}