// 
// AspNetParsingTests.cs
//  
// Author:
//       Michael Hutchinson <mhutchinson@novell.com>
// 
// Copyright (c) 2009 Novell, Inc. (http://www.novell.com)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Linq;
using MonoDevelop.AspNet.StateEngine;
using NUnit.Framework;

namespace MonoDevelop.Xml.StateEngine
{
	[TestFixture]
	public class AspNetParsingTests : HtmlParsingTests
	{
		public override XmlFreeState CreateRootState ()
		{
			return new AspNetFreeState ();
		}

		[Test]
		public void Directives ()
		{
			var parser = new TestParser (CreateRootState (), true);
			parser.Parse (@"<%@ Page Language=""C#"" %>");
			parser.AssertNoErrors ();
			var doc = (XDocument) parser.Nodes.Peek ();
			var directive = doc.Nodes.First () as AspNetDirective;
			Assert.NotNull (directive);
			Assert.AreEqual ("Page", directive.Name.FullName);
			Assert.AreEqual (1, directive.Attributes.Count ());
			var att = directive.Attributes.First ();
			Assert.AreEqual ("Language", att.Name.FullName);
			Assert.AreEqual ("C#", att.Value);
		}
	}
}