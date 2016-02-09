
# Image

An image. What it's all about.


```
/customers/{0}/spaces/{1}/images/{2}
```


## Supported operations


|Method|Label|Expects|Returns|Status|
|--|--|--|--|--|
|GET|Retrieve a Image| |vocab:Image| |
|PUT|create or replace a Image|vocab:Image|vocab:Image| |
|PATCH|Update the supplied fields of the Image|vocab:Image|vocab:Image|200 patched Image|
|DELETE|Delete the Image| |owl:Nothing| |


## Supported properties


### modelId

The identifier for the image within the space - its URI component


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Image|xsd:string|False|False|


### space

The identifier for the space within the customer - its URI component


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Image|xsd:integer|False|False|


### infoJson

info.json URI


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Image|xsd:string|False|False|


### degradedInfoJson

Degraded info.json URI


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Image|xsd:string|False|False|


### thumbnailInfoJson

Thumbnail info.json URI


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Image|xsd:string|False|False|


### thumbnail400

Direct URI of the 400 pixel thumbnail


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Image|xsd:string|True|False|


### created

Date the image was added


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Image|xsd:dateTime|True|False|


### origin

Origin endpoint from where the original image can be acquired


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Image|xsd:string|False|False|


### initialOrigin

Endpoint to use the first time the image is retrieved


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Image|xsd:string|False|False|


### maxUnauthorised

Maximum size of request allowed before roles are enforced - relates to the effective WHOLE image size, not the individual tile size. 0 = No open option, -1 (default) = no authorisation


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Image|xsd:integer|False|False|


### width

Tile source width


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Image|xsd:integer|True|False|


### height

Tile source height


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Image|xsd:integer|True|False|


### queued

When the image was added to the queue


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Image|xsd:dateTime|True|False|


### dequeued

When the image was taken off the queue


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Image|xsd:dateTime|True|False|


### finished

When the image processing finished (image ready)


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Image|xsd:dateTime|True|False|


### ingesting

Is the image currently being ingested?


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Image|xsd:boolean|True|False|


### error

Reported errors with this image


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Image|xsd:string|False|False|


### tags

Image tags


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Image|xsd:string|False|False|


### string1

String reference 1


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Image|xsd:string|False|False|


### string2

String reference 2


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Image|xsd:string|False|False|


### string3

String reference 3


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Image|xsd:string|False|False|


### number1

Number reference 1


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Image|xsd:nonNegativeInteger|False|False|


### number2

Number reference 2


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Image|xsd:nonNegativeInteger|False|False|


### number3

Number reference 3


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Image|xsd:nonNegativeInteger|False|False|


### roles (🔗)

The role or roles that a user must possess to view this image above maxUnauthorised


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Image|hydra:Collection|False|False|


```
/customers/{0}/spaces/{1}/images/{2}/roles
```


|Method|Label|Expects|Returns|Status|
|--|--|--|--|--|
|GET|Retrieves all Role| |hydra:Collection| |
|POST|Creates a new Role|vocab:Role|vocab:Role|201 Role created.|


### batch (🔗)

The batch this image was ingested in (most recently). Might be blank if the batch has been archived or the image as ingested in immediate mode.


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Image|vocab:Batch|True|False|


```
/customers/{0}/spaces/{1}/images/{2}/batch
```


|Method|Label|Expects|Returns|Status|
|--|--|--|--|--|
|GET|Retrieve a Batch| |vocab:Image| |
|PUT|create or replace a Batch|vocab:Image|vocab:Image| |
|PATCH|Update the supplied fields of the Batch|vocab:Image|vocab:Image|200 patched Batch|
|DELETE|Delete the Batch| |owl:Nothing| |

