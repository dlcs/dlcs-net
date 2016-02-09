
# EntryPoint

The main entry point or homepage of the API.


```

```


## Supported operations


|Method|Label|Expects|Returns|Statuses|
|--|--|--|--|--|
|GET|The API's main entry point.| |vocab:EntryPoint| |


## Supported properties


### customers (🔗)

List of customers to which you have access (usually 1)


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:EntryPoint|hydra:Collection|True|False|


```
/customers
```


|Method|Label|Expects|Returns|Statuses|
|--|--|--|--|--|
|GET|Retrieves all Customer entities| |hydra:Collection| |

