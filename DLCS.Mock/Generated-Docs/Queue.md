
# Queue

Your current ingesting images


```
/customers/{0}/queue
```


## Supported operations


|Method|Label|Expects|Returns|Status|
|--|--|--|--|--|
|GET|Returns the queue resource| |vocab:Queue| |
|POST|Submit an array of Image and get a batch back|hydra:Collection|vocab:Batch|202 Job has been accepted|


## Supported properties


### size

Number of total images in your queue, across batches


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Queue|xsd:nonNegativeInteger|True|False|


### batches (🔗)


```
/customers/{0}/queue/batches
```

Separate jobs you have submitted


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Queue|hydra:Collection|True|False|


|Method|Label|Expects|Returns|Status|
|--|--|--|--|--|
|GET|Retrieves all batches for customer| |hydra:Collection| |


### images (🔗)


```
/customers/{0}/queue/images
```

Merged view of images on the queue, across batches


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Queue|hydra:Collection|True|False|

