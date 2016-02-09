
# Customer

A customer represents you, the API user. You only have access to one customer, so it is your effective entry point for the API. 


```
/customers/{0}
```


## Supported operations


|Method|Label|Expects|Returns|Status|
|--|--|--|--|--|
|GET|Retrieve a Customer| |vocab:Customer| |
|PATCH|Update the supplied fields of the Customer|vocab:Customer|vocab:Customer|200 patched Customer|


## Supported properties


### name

The URL-friendly name of the customer


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Customer|xsd:string|False|False|


### displayName

The display name of the customer


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Customer|xsd:string|False|False|


### portalUsers (🔗)


```
/customers/{0}/portalUsers
```

Accounts that can log into the portal


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Customer|hydra:Collection|True|False|


|Method|Label|Expects|Returns|Status|
|--|--|--|--|--|
|GET|Retrieves all Portal User| |hydra:Collection| |
|POST|Creates a new Portal User|vocab:PortalUser|vocab:PortalUser|201 Portal User created.|


### namedQueries (🔗)


```
/customers/{0}/namedQueries
```

Set of preconfigured URI patterns that will generate IIIF resources on the main DLCS site


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Customer|hydra:Collection|True|False|


|Method|Label|Expects|Returns|Status|
|--|--|--|--|--|
|GET|Retrieves all Named Query| |hydra:Collection| |
|POST|Creates a new Named Query|vocab:NamedQuery|vocab:NamedQuery|201 Named Query created.|


### originStrategies (🔗)


```
/customers/{0}/originStrategies
```

Configuration for retrieving images from your endpoint(s)


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Customer|hydra:Collection|True|False|


|Method|Label|Expects|Returns|Status|
|--|--|--|--|--|
|GET|Retrieves all Origin Strategy| |hydra:Collection| |
|POST|Creates a new Origin Strategy|vocab:OriginStrategy|vocab:OriginStrategy|201 Origin Strategy created.|


### authServices (🔗)


```
/customers/{0}/authServices
```

Configuration for IIIF Auth Services available to you


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Customer|hydra:Collection|True|False|


|Method|Label|Expects|Returns|Status|
|--|--|--|--|--|
|GET|Retrieves all Auth Service| |hydra:Collection| |
|POST|Creates a new Auth Service|vocab:AuthService|vocab:AuthService|201 Auth Service created.|


### roles (🔗)


```
/customers/{0}/roles
```

The set of roles you have registered for the DLCS to enforce access control


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Customer|hydra:Collection|True|False|


|Method|Label|Expects|Returns|Status|
|--|--|--|--|--|
|GET|Retrieves all Space| |hydra:Collection| |
|POST|Creates a new Space|vocab:Space|vocab:Space|201 Space created.|


### queue (🔗)


```
/customers/{0}/queue
```

The Customer's view on the DLCS ingest queue


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Customer|vocab:Queue|True|False|


|Method|Label|Expects|Returns|Status|
|--|--|--|--|--|
|GET|Returns the queue resource| |vocab:Queue| |
|POST|Submit an array of Image and get a batch back|hydra:Collection|vocab:Batch|202 Job has been accepted|


### spaces (🔗)


```
/customers/{0}/spaces
```

A space allows you to partition images, have different default rules, etc


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Customer|hydra:Collection|True|False|

