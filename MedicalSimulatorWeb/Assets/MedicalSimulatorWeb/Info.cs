//This file for info

//Test site: http://www.filatik.somee.com/

//To run the application, you need to load all the files from the assembly in the file manager and additionally the config file

//web.config file setting from https://docs.unity3d.com/2020.1/Documentation/Manual/webgl-server-configuration-code-samples.html

//Server configuration for compressed WebGL builds without decompression fallback (IIS)
/*
<?xml version="1.0" encoding="UTF-8"?>
<!--
 The following server configuration can be used for compressed WebGL builds without decompression fallback.
 This configuration file should be uploaded to the server as "<Application Folder>/Build/web.config".
 This configuration has been tested with Unity 2020.1 builds, hosted on IIS 7.5, IIS 8.5, and IIS 10.0.
 NOTE: To host compressed WebGL builds without decompression fallback,
you need to install the "URL Rewrite" IIS module on the server.
Otherwise, IIS will throw an exception when using this configuration file.
This module is available at https://www.iis.net/downloads/microsoft/url-rewrite.
-->


<configuration>
 <system.webServer>
   <!--
     Compressed Unity builds without decompression fallback can't be properly hosted on a server which
     has static compression enabled because this might result in the build files being compressed twice.
     The following line disables static server compression.
   -->
   <urlCompression doStaticCompression="false" />
   <!-- To host compressed Unity builds, the correct mimeType should be set for the compressed build files. -->
   <staticContent>
     <!--
       NOTE: IIS will throw an exception if a mimeType is specified multiple times for the same extension.
       To avoid possible conflicts with configurations that are already on the server, you should remove the mimeType for the corresponding extension using the <remove> element,
       before adding mimeType using the <mimeMap> element.
     -->
     <!-- The following lines are required for builds compressed with gzip, which don't include decompression fallback. -->
     <remove fileExtension=".data.gz" />
     <mimeMap fileExtension=".data.gz" mimeType="application/octet-stream" />
     <remove fileExtension=".wasm.gz" />
     <mimeMap fileExtension=".wasm.gz" mimeType="application/wasm" />
     <remove fileExtension=".js.gz" />
     <mimeMap fileExtension=".js.gz" mimeType="application/javascript" />
     <remove fileExtension=".symbols.json.gz" />
     <mimeMap fileExtension=".symbols.json.gz" mimeType="application/octet-stream" />
     <!-- The following lines are required for builds compressed with Brotli, which don't include decompression fallback. -->
     <remove fileExtension=".data.br" />
     <mimeMap fileExtension=".data.br" mimeType="application/octet-stream" />
     <remove fileExtension=".wasm.br" />
     <mimeMap fileExtension=".wasm.br" mimeType="application/wasm" />
     <remove fileExtension=".js.br" />
     <mimeMap fileExtension=".js.br" mimeType="application/javascript" />
     <remove fileExtension=".symbols.json.br" />
     <mimeMap fileExtension=".symbols.json.br" mimeType="application/octet-stream" />
   </staticContent>

   <!--
     Hosting compressed Unity builds without decompression fallback relies on native browser decompression,
     therefore a proper "Content-Encoding" response header should be added for the compressed build files.
     NOTE: IIS will throw an exception if the following section is used without the "URL Rewrite" module installed.
     Download the "URL Rewrite" module from https://www.iis.net/downloads/microsoft/url-rewrite
   -->
   <rewrite>
     <outboundRules>
       <!--
         NOTE: IIS will throw an exception if the same rule name is used multiple times.
         To avoid possible conflicts with configurations that are already on the server, you should remove the mimeType for the corresponding extension using the <remove> element,
       before adding mimeType using the <mimeMap> element.
       -->
       <!-- The following section is required for builds compressed with gzip, which don't include decompression fallback. -->
       <remove name="Append gzip Content-Encoding header" />
       <rule name="Append gzip Content-Encoding header">
         <match serverVariable="RESPONSE_Content-Encoding" pattern=".*" />
         <conditions>
           <add input="{REQUEST_FILENAME}" pattern="\.gz$" />
         </conditions>
         <action type="Rewrite" value="gzip" />
       </rule>
       <!-- The following section is required for builds compressed with Brotli, which don't include decompression fallback. -->
       <remove name="Append brotli Content-Encoding header" />
       <rule name="Append brotli Content-Encoding header">
         <match serverVariable="RESPONSE_Content-Encoding" pattern=".*" />
         <conditions>
           <add input="{REQUEST_FILENAME}" pattern="\.br$" />
         </conditions>
         <action type="Rewrite" value="br" />
       </rule>
     </outboundRules>
   </rewrite>
 </system.webServer>
</configuration>
*/

//Server configuration for uncompressed WebGL builds (IIS)
/*
<?xml version="1.0" encoding="UTF-8"?>
<!--
 The following server configuration can be used for uncompressed WebGL builds.
 This configuration file should be uploaded to the server as "<Application Folder>/Build/web.config"
 This configuration has been tested with Unity 2020.1 builds, hosted on IIS 7.5, IIS 8.5, and IIS 10.0.
-->
<configuration>
 <system.webServer>
   <!--
     IIS does not provide default handlers for .data and .wasm files (and in some cases .json files),
     therefore these files won’t be served unless their mimeType is explicitly specified.
   -->
   <staticContent>
     <!--
       NOTE: IIS will throw an exception if a mimeType is specified multiple times for the same extension.
       To avoid possible conflicts with configurations that are already on the server, you should remove the mimeType for the corresponding extension using the <remove> element,
       before adding mimeType using the <mimeMap> element.
     -->
     <remove fileExtension=".data" />
     <mimeMap fileExtension=".data" mimeType="application/octet-stream" />
     <remove fileExtension=".wasm" />
     <mimeMap fileExtension=".wasm" mimeType="application/wasm" />
     <remove fileExtension=".symbols.json" />
     <mimeMap fileExtension=".symbols.json" mimeType="application/octet-stream" />
   </staticContent>
 </system.webServer>
</configuration>
*/

//Server configuration for WebGL builds (Apache)
/*
# This configuration file should be uploaded to the server as "<Application Folder>/Build/.htaccess"
# This configuration has been tested with Unity 2020.1 builds, hosted on Apache/2.4
# NOTE: "mod_mime" Apache module must be enabled for this configuration to work.
<IfModule mod_mime.c>

# The following lines are required for builds without decompression fallback, compressed with gzip
RemoveType .gz
AddEncoding gzip .gz
AddType application/octet-stream .data.gz
AddType application/wasm .wasm.gz
AddType application/javascript .js.gz
AddType application/octet-stream .symbols.json.gz

# The following lines are required for builds without decompression fallback, compressed with Brotli
RemoveType .br
RemoveLanguage .br
AddEncoding br .br
AddType application/octet-stream .data.br
AddType application/wasm .wasm.br
AddType application/javascript .js.br
AddType application/octet-stream .symbols.json.br

# The following line improves loading performance for uncompressed builds
AddType application/wasm .wasm

# Uncomment the following line to improve loading performance for gzip-compressed builds with decompression fallback
# AddEncoding gzip .unityweb

# Uncomment the following line to improve loading performance for brotli-compressed builds with decompression fallback
# AddEncoding br .unityweb
*/