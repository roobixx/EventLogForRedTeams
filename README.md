 EventLogForRedTeams is a proof-of-concept for showing how a malicious pyalod can be stored in a Windows event log entry and later retieved for execution. This technique is not original, and was first discuseed here: https://threatpost.com/attackers-use-event-logs-to-hide-fileless-malware/179484/ and https://securelist.com/a-new-secret-stash-for-fileless-malware/106393/
 
 
 - Generate Shellcode
 - Create Event Log Entry
 - Compile .exe
 - Execute
 - Profit?

![Injecting Payload into Event Log](img/msf.png)

Powershell Write-EventLog Command
```powershell
Write-Event -LogName $1 -Source $2 -EventID $3 -EventType Information -Category 0 -Message $4 -RawData $5
```

