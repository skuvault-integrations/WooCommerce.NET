using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using WooCommerceNET;
using WooCommerceNET.WooCommerce.v3;

namespace WooCommerce.NET.Tests
{
	[TestFixture]
	public class RestApiTests
	{
		[Test]
		public void DeserializeJSon_ProductsHaveDateTimeMinValue_WhenDatesAreIncorrect()
		{
			// arrange
			var restApiV3 = new RestAPI("wp-json/wc/v3", "", "");

			var jsonResponse =
				File.ReadAllText(
					@"..\..\Files\ProductsJsonResponse_WhenDatesAreIncorrect.txt");

			// act
			var products = restApiV3.DeserializeJSon<List<Product>>(jsonResponse);

			// assert
			Assert.That(products.Count, Is.EqualTo(1));
			Assert.That(products[0].date_created, Is.EqualTo(DateTime.MinValue));
			Assert.That(products[0].date_created_gmt, Is.EqualTo(DateTime.MinValue));
			Assert.That(products[0].date_modified, Is.EqualTo(DateTime.MinValue));
			Assert.That(products[0].date_modified_gmt, Is.EqualTo(DateTime.MinValue));
			Assert.That(products[0].date_on_sale_from, Is.EqualTo(DateTime.MinValue));
			Assert.That(products[0].date_on_sale_from_gmt, Is.EqualTo(DateTime.MinValue));
			Assert.That(products[0].date_on_sale_to, Is.EqualTo(DateTime.MinValue));
			Assert.That(products[0].date_on_sale_to_gmt, Is.EqualTo(DateTime.MinValue));
			Assert.That(products[0].images[0].date_created, Is.EqualTo(DateTime.MinValue));
			Assert.That(products[0].images[0].date_created_gmt, Is.EqualTo(DateTime.MinValue));
			Assert.That(products[0].images[0].date_modified, Is.EqualTo(DateTime.MinValue));
			Assert.That(products[0].images[0].date_modified_gmt, Is.EqualTo(DateTime.MinValue));
		}

		[Test]
		public void DeserializeJSon_ProductsHaveCorrectDateTimeValues_WhenDatesAreCorrect()
		{
			// arrange
			var restApiV3 = new RestAPI("wp-json/wc/v3", "", "");

			var jsonResponse =
				File.ReadAllText(
					@"..\..\Files\ProductsJsonResponse_WhenDatesAreCorrect.txt");

			// act
			var products = restApiV3.DeserializeJSon<List<Product>>(jsonResponse);

			// assert
			var correctDateTimeValue = new DateTime(2023, 11, 29, 19, 03, 58);
			Assert.That(products.Count, Is.EqualTo(1));
			Assert.That(products[0].date_created, Is.EqualTo(correctDateTimeValue));
			Assert.That(products[0].date_created_gmt, Is.EqualTo(correctDateTimeValue));
			Assert.That(products[0].date_modified, Is.EqualTo(correctDateTimeValue));
			Assert.That(products[0].date_modified_gmt, Is.EqualTo(correctDateTimeValue));
			Assert.That(products[0].date_on_sale_from, Is.EqualTo(correctDateTimeValue));
			Assert.That(products[0].date_on_sale_from_gmt, Is.EqualTo(correctDateTimeValue));
			Assert.That(products[0].date_on_sale_to, Is.EqualTo(correctDateTimeValue));
			Assert.That(products[0].date_on_sale_to_gmt, Is.EqualTo(correctDateTimeValue));
			Assert.That(products[0].images[0].date_created, Is.EqualTo(correctDateTimeValue));
			Assert.That(products[0].images[0].date_created_gmt, Is.EqualTo(correctDateTimeValue));
			Assert.That(products[0].images[0].date_modified, Is.EqualTo(correctDateTimeValue));
			Assert.That(products[0].images[0].date_modified_gmt, Is.EqualTo(correctDateTimeValue));
		}
	}
}