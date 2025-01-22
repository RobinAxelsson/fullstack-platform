# TenStar - System developer – Evaluation test

## Summary
This test will evaluate your skills as a full stack developer and will cover everything between
database to frontend.

The task is to write a function to upload a csv file to a web application and save it to a database.

The test time scope is approximately 12 hours, but you are free to spend more or less time as
needed. However, we would like your results to be returned within 7 calendar days of receiving
the test.

## Specification
Create a web application where you can upload a csv file containing users, users should have
the following fields:
- Full name, has maximum length of 100 characters
- Username, has maximum length of 100 characters
- Email, must be formatted as an email address ex. something@example.com
- Password, must contain at least one upper case letter, one lower case letter, one digit and one special character, it also must be longer than 8 characters.
All the fields are required.

When the file has been uploaded the users should be presented as a table and the fields that are not valid should be highlighted.

There must be a button to save the users to a database.
The solution should also contain unit tests for where applicable.

## Environment
- Backend should be .NET using C#
- Frontend could be Angular or Blazor
- For database mapping EF core is preferred

## Deliverables
- Source code, delivered using Google Drive, OneDrive or similar
- A sample csv for import delivered alongside the source code
- Information on how much time you spent on the test in total
