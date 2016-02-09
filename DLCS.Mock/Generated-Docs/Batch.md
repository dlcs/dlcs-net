
# Batch

Represents a submitted job of images


```
/customers/{0}/queue/batches/{1}
```


## Supported operations


|Method|Label|Expects|Returns|Status|
|--|--|--|--|--|
|GET|Retrieve a Batch| |vocab:Batch| |
|DELETE|Delete the Batch| |owl:Nothing| |


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


```
/customers/{0}/queue/batches/{1}/images
```

All the images in the batch


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Batch|hydra:Collection|True|False|


|Method|Label|Expects|Returns|Status|
|--|--|--|--|--|
|GET|Retrieves all images in batch regardless of state| |hydra:Collection| |


### completedImages (🔗)


```
/customers/{0}/queue/batches/{1}/completedImages
```

Images that have completed processing


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Batch|hydra:Collection|True|False|


|Method|Label|Expects|Returns|Status|
|--|--|--|--|--|
|GET|Retrieves all COMPLETED images in batch| |hydra:Collection| |


### errorImages (🔗)


```
/customers/{0}/queue/batches/{1}/errorImages
```

Images that encountered errors


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Batch|hydra:Collection|True|False|


|Method|Label|Expects|Returns|Status|
|--|--|--|--|--|
|GET|Retrieves all ERROR images in batch| |hydra:Collection| |

