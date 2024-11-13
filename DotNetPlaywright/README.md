# Testing with .NET Playwright

Author: IxxI5

### Description

This NUnit test demonstrates basic Playwright capabilities for testing a Web API, specifically the DummyJSON API (https://dummyjson.com). It covers the use of the POST method for logging in to obtain authentication tokens, and the GET method to retrieve the current authenticated user's details by using the obtained tokens. This example showcases Playwright’s ability to handle simple API interactions, such as sending and receiving data using POST and GET requests, making it a useful tool for testing web services.

### Launch a Test

- **Unzip** the downloaded project from Github
- **Open** the solution in Visual Studio
- **Install Dependencies and Playwright Browsers** through Developer's Powershell:
  ```
  dotnet install		// install all the dependencies
  
  playwright install	// download the necessary Playwright browsers
  ```
- **Run a Test in Headless Mode** through Developer's Powershell (or Test Explorer UI):

  ```
  dotnet test					// runs all tests
 
  dotnet test --filter "Category=smoke"		// runs all tests with category="smoke"
  ```

### Create a .NET Playwright Project Step by Step

- CREATE a New NUnit Cross Platform Project through Visual Studio UI
  
  Alternatively, execute the following commands through the Developer's PowerShell:
  ```
  dotnet new nunit -o myNUnitPlaywrightProject
 
  cd myNUnitPlaywrightProject
 
  dotnet add package Microsoft.Playwright

  dotnet add package FluentAssertions
 
  dotnet playwright install
 
  dotnet tool install --global Microsoft.Playwright.CLI	// you install it once, globally

  playwright install

  ```

## License

[![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](https://choosealicense.com/licenses/mit/)

Copyright (c) 2015 Chris Kibble

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.