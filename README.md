# PowerDiaryTest

<b>Notes</b>

Tested in VS2013, VS2015, VS2017

There are two DB connection strings in Web.config file.

For VS2015, VS2017 


`    <add name="EFDbContext" connectionString="Data Source=(localdb)\MSSQLLocalDB;
Initial Catalog=msdb;Integrated Security=True" providerName="System.Data.SqlClient" />`

For VS2013

`<add name="EFDbContext" connectionString="Data Source=(LocalDb)\V11.0;
    Initial Catalog=msdb;Integrated Security=True" providerName="System.Data.SqlClient" />`

By default VS2015,VS2017 connection string is used.


<b>Known issues:</b>

Sometimes there is an exception at startup: 

"
System.AccessViolationException was unhandled.
Message: An unhandled exception of type 'System.AccessViolationException' occurred in MsieJavaScriptEngine.dll
Additional information: Attempted to read or write protected memory. This is often an indication that other memory is corrupt.
"


Seems well known in 'React'

https://github.com/reactjs/React.NET/issues/281

