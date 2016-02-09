
# Queue

Your current ingesting images


```javascript
/customers/{0}/queue
```


## Supported properties


### size

Number of total images in your queue, across batches


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Queue|xsd:nonNegativeInteger|True|False|


### batches

Separate jobs you have submitted


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Queue|hydra:Collection|True|False|

Supported operations on link:

**Template: **
```javascript
/customers/{0}/queue/batches
```


|Method|Label|Expects|Returns|Status|
|--|--|--|--|--|
|GET|Retrieves all batches for customer||hydra:Collection||


### images

Merged view of images on the queue, across batches


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Queue|hydra:Collection|True|False|

Supported operations on link:

**Template: **
```javascript
/customers/{0}/queue/images
```


## Supported operations


```javascript
/customers/{0}/queue
```


|Method|Label|Expects|Returns|Status|
|--|--|--|--|--|
|GET|Returns the queue resource||vocab:Queue||
|POST|Submit an array of Image and get a batch back|hydra:Collection|vocab:Batch|202 Job has been accepted|

