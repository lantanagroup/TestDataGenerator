# TestDataGenerator

TODO: Description

## Examples

```bash
TestDataGeneration.exe -g:..\TestData\NCI\good-files -b:..\TestData\NCI\bad-files -e:..\TestData\NCI\messages.xml -d:..\TestData\NCI\TestDataSource.xls -m:..\TestData\NCI\sample.xml -ac:..\TestData\NCI\action_config.xml
TestDataGeneration.exe -b:..\TestData\NCI\bad-files -e:..\TestData\NCI\messages.xml -d:..\TestData\NCI\TestDataSource.xls -m:..\TestData\NCI\sample.xml -ac:..\TestData\NCI\action_config.xml
TestDataGeneration.exe -g:..\TestData\NCI\good-files -e:..\TestData\NCI\messages.xml -d:..\TestData\NCI\TestDataSource.xls -m:..\TestData\NCI\sample.xml -ac:..\TestData\NCI\action_config.xml
TestDataGeneration.exe -g:..\TestData\Consolidation\Output -e:..\TestData\Consolidation\messages.xml -d:..\TestData\Consolidation\DataSource.xls -m:..\TestData\Consolidation\sample.xml -ac:..\TestData\Consolidation\config.xml
```

## Debugging Notes:
- This is the arguments that should be used when debugging: "-g:good-files -b:bad-files -e:messages.xml -d:..\..\..\TestDataGenerationToolTests\data\TestDataSource.xls -m:..\..\..\TestDataGenerationToolTests\data\sample-tokenized.xml"

## Action Configuration Notes:
- xsd /namespace:LantanaGroup.TestDataGenerationTool.Data /classes /language:C# ActionConfiguration.xsd
-- After generating classes, manually changed the classes to have proper casing
- The location attribute, when referencing an element or a set of nodes, it must not end in a backslash (/), or the XPATH parser will complain that it doesn't return a node-set.

## Excel notes:
- Sometimes excel formats numbers oddly in the configuration tab and the tool can't read them. Currently have to completely remove the row and add it back. Seems to work then.
- Sheets cannot have spaces in their names.