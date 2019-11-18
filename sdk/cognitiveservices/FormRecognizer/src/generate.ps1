Start-AutoRestCodeGeneration -ResourceProvider "cognitiveservices/data-plane/FormRecognizer" -AutoRestVersion "latest"

<# 

There are three issues with the generated code that must be manually corrected:

    1.  Swagger uses operation names likes 'FooAsync' which causes unfortunate 'FooAsyncAsync' method names
        Solution: Rename the method

    2.  Autorest tries to JSON-serialize POST payloads that should be delivered in binary form OR as structured JSON
        Solution: Complete method replacement with appropriate overloads to handle Stream and URI payloads

    3.  Listing of custom models uses unfriendly 'op' flag to return different outputs on the same path.
        Solution: Original low-level code-gen becomes private. Expose custom abstractions

#>

$c1 = gc ./IFormRecognizerClient.cs | Out-String
$c2 = gc ./FormRecognizerClientExtensions.cs | Out-String
$c3 = gc ./FormRecognizerClient.cs | Out-String

# Simple method rename
$c1 = $c1 -replace "TrainCustomModelAsyncWithHttpMessagesAsync", "TrainCustomModelWithHttpMessagesAsync"
$c2 = $c2 -replace "TrainCustomModelAsync(WithHttpMessages)?Async", 'TrainCustomModel${1}Async'
$c3 = $c3 -replace "TrainCustomModelAsyncWithHttpMessagesAsync", "TrainCustomModelWithHttpMessagesAsync"

# Remove all three 'Analyze' method definitions. These will be redefined in the Customizations
$c1 = $c1 -replace '(?ms)/// <summary>\s+/// Analyze (?:Form|Receipt|Layout).+?\);', ''
$c2 = $c2 -replace '(?ms)/// <summary>\s+/// Analyze (?:Form|Receipt|Layout).+?}\s+}', ''
$c3 = $c3 -replace '(?ms)/// <summary>\s+/// Analyze (?:Form|Receipt|Layout).+?return _result;\s+}', ''

# Make the 'List Custom Models' method private. This will be exposed via abstraction in the Customizations
$c1 = $c1 -replace '(?ms)/// <summary>\s+/// List Custom Models.+?\);', ''
$c2 = $c2 -replace '(?ms)/// <summary>\s+/// List Custom Models.+?}\s+}', ''
$c3 = $c3 -replace 'public async Task<HttpOperationResponse<ModelsModel>> GetCustomModelsWithHttpMessagesAsync\(', 'private async Task<HttpOperationResponse<ModelsModel>> GetCustomModelsWithHttpMessagesAsync('

$c1 > ./IFormRecognizerClient.cs
$c2 > ./FormRecognizerClientExtensions.cs
$c3 > ./FormRecognizerClient.cs
