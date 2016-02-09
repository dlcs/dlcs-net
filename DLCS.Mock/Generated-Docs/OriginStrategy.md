
# OriginStrategy

Configuration that tells the DLCS how to acquire images from your origin endpoints

```javascript
/customers/{0}/originStrategies/{1}
```


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


## Supported operations

**Template: **```javascript
/customers/{0}/originStrategies/{1}
```


|Method|Label|Expects|Returns|Status|
|--|--|--|--|--|
|GET|Retrieve a Origin Strategy||vocab:OriginStrategy||
|PUT|create or replace a Origin Strategy|vocab:OriginStrategy|vocab:OriginStrategy||
|PATCH|Update the supplied fields of the Origin Strategy|vocab:OriginStrategy|vocab:OriginStrategy|200 patched Origin Strategy|
|DELETE|Delete the Origin Strategy||owl:Nothing||

