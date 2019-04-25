

# JSONPath
This tool is a simple CLI wrapper around Newtonsoft.Json.Net's JPATH parser. It reads
from stdin, evaluates JSONpath expression and outputs json 

## To Install
```dotnet tool install -g jsonpath-cli```

## To use
```jsonpath [JSONPath expression]```

## Example
```type json.txt | jsonpath *[jsonpathexpression]*```

# JsonPath CheatSheet
see https://goessner.net/articles/JsonPath/ for more details on JSONPath syntax

| XPath                | JSONPath               | Description                                                  |
|----------------------|------------------------|--------------------------------------------------------------|
| /store/book/author   | $.store.book[*].author | the authors of all books in the store                        |
| //author             | $..author              | all authors                                                  |
| /store/*             | $.store.*              | all things in store, which are some books and a red bicycle. |
| /store//price        | $.store..price         | the price of everything in the store.                        |
| //book[3]            | $..book[2]             | the third book                                               |
| //book[last()]       | $..book[(@.length-1)]  | the last book in order.                                      |
| $..book[-1:]         |                        |                                                              |
| //book[position()<3] | $..book[0,1]           | the first two books                                          |
| $..book[:2]          |                        |                                                              |
| //book[isbn]         | $..book[?(@.isbn)]     | filter all books with isbn number                            |
| //book[price<10]     | $..book[?(@.price<10)] | filter all books cheapier than 10                            |
| //*                  | $..*                   | all Elements in XML document. All members of JSON structure. |
