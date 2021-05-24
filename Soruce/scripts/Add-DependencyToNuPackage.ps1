Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

$scriptDir = Split-Path ( & {$MyInvocation.ScriptName}) -Parent


Push-Location $scriptDir
try
{
    $nupackagePaths = ls ".." -File "TestingFileUtilities.TypeGenerator.1.0.0.nupkg" -Recurse
    
    foreach($nupackagePath in $nupackagePaths)
    {
        $packagePath = $nupackagePath.FullName;

        $tempDir = Join-Path $scriptDir "temp"
        if(Test-Path -LiteralPath $tempDir)
        {
            Remove-Item -LiteralPath $tempDir -Force -Recurse
            while(Test-Path -LiteralPath $tempDir)
            {
                sleep -Milliseconds 100;
            }
        }

        & "..\tools\7za\7za.exe" "x" "$packagePath" "-o$tempDir"

        $nuspecPath = Join-Path $tempDir "TestingFileUtilities.TypeGenerator.nuspec"

        $contents = Get-Content $nuspecPath -Encoding UTF8


        $addText = @"
    <dependencies>
        <dependency id="TestingFileUtilities" version="1.1.0" />
    </dependencies>
  </metadata>
"@

        $contents = $contents | ?{ 
                -not $_.Contains("<dependencies>") -and -not $_.Contains("<dependency id") -and -not $_.Contains("</dependencies>")
             } | % {
                if($_.Contains("</metadata>")){
                    return $addText;
                }
                else
                {
                    return $_
                }
            }
        
        Set-Content $nuspecPath $contents -Encoding UTF8

        & "..\tools\7za\7za.exe" "a" "$packagePath" "$tempDir\*"
    }
}
finally
{
    $tempDir = Join-Path $scriptDir "temp"
    if(Test-Path -LiteralPath $tempDir)
    {
        Remove-Item -LiteralPath $tempDir -Force -Recurse
    }

    Pop-Location
}