
# Customer

The route to all of your assets in the DLCS


```javascript
/customers/{0}
```


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


### portalUsers

Accounts that can log into the portal


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Customer|hydra:Collection|True|False|

This property is a LINK...


```javascript
/customers/{0}/portalUsers
```


|Method|Label|Expects|Returns|Status|
|--|--|--|--|--|
|GET|Retrieves all Portal User||hydra:Collection||
|POST|Creates a new Portal User|vocab:PortalUser|vocab:PortalUser|201 Portal User created.|


### namedQueries

Set of preconfigured URI patterns that will generate IIIF resources on the main DLCS site


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Customer|hydra:Collection|True|False|

This property is a LINK...


```javascript
/customers/{0}/namedQueries
```


|Method|Label|Expects|Returns|Status|
|--|--|--|--|--|
|GET|Retrieves all Named Query||hydra:Collection||
|POST|Creates a new Named Query|vocab:NamedQuery|vocab:NamedQuery|201 Named Query created.|


### originStrategies

Configuration for retrieving images from your endpoint(s)


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Customer|hydra:Collection|True|False|

This property is a LINK...


```javascript
/customers/{0}/originStrategies
```


|Method|Label|Expects|Returns|Status|
|--|--|--|--|--|
|GET|Retrieves all Origin Strategy||hydra:Collection||
|POST|Creates a new Origin Strategy|vocab:OriginStrategy|vocab:OriginStrategy|201 Origin Strategy created.|


### authServices

Configuration for IIIF Auth Services available to you


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Customer|hydra:Collection|True|False|

This property is a LINK...


```javascript
/customers/{0}/authServices
```


|Method|Label|Expects|Returns|Status|
|--|--|--|--|--|
|GET|Retrieves all Auth Service||hydra:Collection||
|POST|Creates a new Auth Service|vocab:AuthService|vocab:AuthService|201 Auth Service created.|


### roles

The set of roles you have registered for the DLCS to enforce access control


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Customer|hydra:Collection|True|False|

This property is a LINK...


```javascript
/customers/{0}/roles
```


|Method|Label|Expects|Returns|Status|
|--|--|--|--|--|
|GET|Retrieves all Space||hydra:Collection||
|POST|Creates a new Space|vocab:Space|vocab:Space|201 Space created.|


### queue

The Customer's view on the DLCS ingest queue


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Customer|vocab:Queue|True|False|

This property is a LINK...


```javascript
/customers/{0}/queue
```


|Method|Label|Expects|Returns|Status|
|--|--|--|--|--|
|GET|Returns the queue resource||vocab:Queue||
|POST|Submit an array of Image and get a batch back|hydra:Collection|vocab:Batch|202 Job has been accepted|


### spaces

A space allows you to partition images, have different default rules, etc


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Customer|hydra:Collection|True|False|

This property is a LINK...


```javascript
/customers/{0}/spaces
```


## Supported operations


```javascript
/customers/{0}
```


|Method|Label|Expects|Returns|Status|
|--|--|--|--|--|
|GET|Retrieve a Customer||vocab:Customer||
|PATCH|Update the supplied fields of the Customer|vocab:Customer|vocab:Customer|200 patched Customer|

