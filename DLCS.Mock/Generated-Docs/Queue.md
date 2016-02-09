
# Queue

Your current ingesting images


```
/customers/{0}/queue
```


## Supported operations


|Method|Label|Expects|Returns|Statuses|
|--|--|--|--|--|
|GET|Returns the queue resource| |vocab:Queue| |
|POST|Submit an array of Image and get a batch back|hydra:Collection|vocab:Batch|201 Job has been accepted - Batch created and returned|


## Supported properties


### size

Number of total images in your queue, across batches


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Queue|xsd:nonNegativeInteger|True|False|


### batches (🔗)

Separate jobs you have submitted


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Queue|hydra:Collection|True|False|


```
/customers/{0}/queue/batches
```


|Method|Label|Expects|Returns|Statuses|
|--|--|--|--|--|
|GET|Retrieves all batches for customer| |hydra:Collection| |


### images (🔗)

Merged view of images on the queue, across batches


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Queue|hydra:Collection|True|False|


```
/customers/{0}/queue/images
```


|Method|Label|Expects|Returns|Statuses|
|--|--|--|--|--|
|GET|Retrieves all images across batches for customer| |hydra:Collection| |

