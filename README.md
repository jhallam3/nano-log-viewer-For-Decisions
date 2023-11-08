NanoLogViewer
=============

GUI viewer for JSON log files (Windows).

![ScreenShot](https://raw.github.com/yar3333/nano-log-viewer/master/screenshot.png)

Can read local files and http(s) sources.

supports calls using the Decisions session option - http://Host/Primary/restapi/Flow/01HEJK952EP3FV791H7SPK0Z27?sessionid={SESSIONID}&outputtype=JSON

Log file format:
* one json object per line;
* json must be in `{}`.

Log example
```json
{ "level":"TRACE", "thread":"TREAD_A",  "message":{ "file":"myfile1.php", "line":10, "stack":"many\nmany\nlines" } }
{ "level":"DEBUG", "thread":"TREAD_B",  "message":{ "file":"myfile2.php", "line":20, "stack":"many\nmany\nlines" } }
```

