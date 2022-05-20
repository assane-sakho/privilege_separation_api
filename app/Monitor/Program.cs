using System.Configuration;
using System.Diagnostics;
using System.IO.Pipes;
using ClassLibrary;
using Monitor;

//Start child process
ProcessStartInfo startInfo = new ProcessStartInfo() { 
    FileName = "/bin/su", 
    Arguments = "nonrootuser -c \"dotnet /app/out/Child.dll\""
}; 
Process child = new Process() { 
    StartInfo = startInfo,
};
child.Start();

DbHelper instance = DbHelper.GetInstance();

while (true)
{
    try
    {
        NamedPipeServerStream pipeServer =
            new NamedPipeServerStream("testpipe", PipeDirection.InOut, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);

        // We wait for a child to connect
        pipeServer.WaitForConnection();

        StreamString ss = new StreamString(pipeServer);

        // We verify our identity to the connected child using a string that the child anticipates.

        ss.WriteString(Environment.GetEnvironmentVariable("serverSignature"));

        string token = ss.ReadString();

        Console.WriteLine($"Token received : {token}");

        string content = "";

        //If the token is invalid, we send back an unauthorized to the child process
        if (!instance.TokenIsValid(token))
        {
            content = "unauthorized";
        }
        else
        {
            string rolesId = instance.GetRolesId(token);
            content = instance.GetSalesData(rolesId);
        }

        ss.WriteString(content);

        pipeServer.Close();
    }
    // Catch the IOException that is raised if the pipe is broken
    // or disconnected.
    catch (IOException e)
    {
        Console.WriteLine("ERROR: {0}", e.Message);
    }
}