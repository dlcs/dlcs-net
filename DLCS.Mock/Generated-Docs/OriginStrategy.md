
# OriginStrategy

As a customer you can provide information to the DLCS to allow it to fetch your images from their origin endpoints. Every customer has a default origin strategy, which is for the DLCS to attempt to fetch the image from its origin URL without presenting credentials. This is fine for images that are publicly available, but is unlikely to be appropriate for images you are exposing from your asset management system.You might have a service that is available only to the DLCS, or an FTP site.


```
/customers/{0}/originStrategies/{1}
```


## Supported operations


|Method|Label|Expects|Returns|Statuses|
|--|--|--|--|--|
|GET|Retrieve a Origin Strategy| |vocab:OriginStrategy|200 OK, 404 Not found|
|PUT|create or replace a Origin Strategy|vocab:OriginStrategy|vocab:OriginStrategy|200 OK, 201 Created Origin Strategy, 404 Not found|
|PATCH|Update the supplied fields of the Origin Strategy|vocab:OriginStrategy|vocab:OriginStrategy|205 Accepted Origin Strategy, reset view, 400 Bad request, 404 Not found|
|DELETE|Delete the Origin Strategy| |owl:Nothing|205 Accepted Origin Strategy, reset view, 404 Not found|


## Supported properties


### regex

Regex for matching origin


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:OriginStrategy|xsd:string|False|False|


### protocol

The protocol to use, if it can't be deduced from the regex


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:OriginStrategy|xsd:string|False|False|


### credentials

JSON object - credentials appropriate to the protocol, will vary


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:OriginStrategy|xsd:string|False|False|

