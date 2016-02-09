
# Batch

Represents a submitted job of images


```
/customers/{0}/queue/batches/{1}
```


## Supported operations


|Method|Label|Expects|Returns|Statuses|
|--|--|--|--|--|
|GET|Retrieve a Batch| |vocab:Batch|200 OK, 404 Not found|


## Supported properties


### submitted

Date the batch was POSTed


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Batch|xsd:dateTime|True|False|


### count

Total number of images in the batch


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Batch|xsd:nonNegativeInteger|True|False|


### completed

Total number of completed images in the batch


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Batch|xsd:nonNegativeInteger|True|False|


### finished

Date the batch was finished (may still have errors)


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Batch|xsd:dateTime|True|False|


### errors

Total number of error images in the batch


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Batch|xsd:nonNegativeInteger|True|False|


### estCompletion

Estimated Completion (best guess as to when this batch might get done)


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Batch|xsd:dateTime|True|False|


### images (🔗)

All the images in the batch


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Batch|hydra:Collection|True|False|


```
/customers/{0}/queue/batches/{1}/images
```


|Method|Label|Expects|Returns|Statuses|
|--|--|--|--|--|
|GET|Retrieves all images in batch regardless of state| |hydra:Collection|200 OK|


### completedImages (🔗)

Images that have completed processing


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Batch|hydra:Collection|True|False|


```
/customers/{0}/queue/batches/{1}/completedImages
```


|Method|Label|Expects|Returns|Statuses|
|--|--|--|--|--|
|GET|Retrieves all COMPLETED images in batch| |hydra:Collection|200 OK|


### errorImages (🔗)

Images that encountered errors


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Batch|hydra:Collection|True|False|


```
/customers/{0}/queue/batches/{1}/errorImages
```


|Method|Label|Expects|Returns|Statuses|
|--|--|--|--|--|
|GET|Retrieves all ERROR images in batch| |hydra:Collection|200 OK|

