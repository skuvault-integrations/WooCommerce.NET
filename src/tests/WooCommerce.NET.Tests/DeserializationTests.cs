using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using WooCommerceNET;
using WooCommerceNET.WooCommerce.v3;

namespace WooCommerce.NET.Tests
{
	[TestFixture]
	public class DeserializationTests
	{
		[TestCase(
			@"..\..\Files\ProductsJsonResponse_WhenDatesAreIncorrect.json",
			"0001-01-01T00:00:00",
			TestName = "DeserializeJSon_ProductsHaveDateTimeMinValue_WhenDatesAreIncorrect")]
		[TestCase(
			@"..\..\Files\ProductsJsonResponse_WhenDatesAreCorrect.json",
			"2023-11-29T19:03:58",
			TestName = "DeserializeJSon_ProductsHaveCorrectDateTimeValues_WhenDatesAreCorrect")]
		[TestCase(
			@"..\..\Files\ProductsJsonResponse_WhenDatesAreNull.json",
			"0001-01-01T00:00:00",
			TestName = "DeserializeJSon_ProductsHaveDateTimeMinValue_WhenDatesAreNull")]
		[TestCase(
			@"..\..\Files\ProductsJsonResponse_WhenDatesAreEmpty.json",
			"0001-01-01T00:00:00",
			TestName = "DeserializeJSon_ProductsHaveDateTimeMinValue_WhenDatesAreEmpty")]
		public void DateTimeDeserializeJson(string fileName, string expectedDateValue)
		{
			// arrange
			var restApiV3 = new RestAPI("wp-json/wc/v3", "", "");

			var jsonResponse = File.ReadAllText(fileName);

			// act
			var products = restApiV3.DeserializeJSon<List<Product>>(jsonResponse);

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
			@"..\..\Files\ProductsJsonResponse_WhenManageStockValueIsParent.json",
			true,
			TestName = "DeserializeJSon_ManageStockIsTrue_WhenManageStockValueIsParent")]
		[TestCase(
			@"..\..\Files\ProductsJsonResponse_WhenManageStockValueIsTrue.json",
			true,
			TestName = "DeserializeJSon_ManageStockIsFalse_WhenManageStockValueIsTrue")]
		[TestCase(
			@"..\..\Files\ProductsJsonResponse_WhenManageStockValueIsNull.json",
			false,
			TestName = "DeserializeJSon_ManageStockIsFalse_WhenManageStockValueIsNull")]
		[TestCase(
			@"..\..\Files\ProductsJsonResponse_WhenManageStockValueIsFalse.json",
			false,
			TestName = "DeserializeJSon_ManageStockIsFalse_WhenManageStockValueIsFalse")]
		public void ManageStockDeserializeJson(string fileName, bool expectedManageStock)
		{
			// arrange
			var restApiV3 = new RestAPI("wp-json/wc/v3", "", "");

			var jsonResponse = File.ReadAllText(fileName);

			// act
			var products = restApiV3.DeserializeJSon<List<Product>>(jsonResponse);

			// assert
			Assert.That(products.Count, Is.EqualTo(1));
			Assert.That(products[0].manage_stock, Is.EqualTo(expectedManageStock));
		}
	}
}