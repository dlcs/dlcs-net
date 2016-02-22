
# Customer

A customer represents you, the API user. You only have access to one customer, so it is your effective entry point for the API. The only interation you can have with your Customer resource directly is updating the display name, but it provides links (🔗) tocollections of all the other resources.


```
/customers/{0}
```


## Supported operations


|Method|Label|Expects|Returns|Statuses|
|--|--|--|--|--|
|GET|Retrieve a Portal Role| |vocab:Customer|200 OK, 404 Not found|


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

Collection of user accounts that can log into the portal. Use this to grant access to others in your organisation


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Customer|hydra:Collection|True|False|


```
/customers/{0}/portalUsers
```


### namedQueries (🔗)

Collection of all the Named Queries you have configured (plus those provided 'out of the box'). See the NamedQuery topic for further information


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Customer|hydra:Collection|True|False|


```
/customers/{0}/namedQueries
```


### originStrategies (🔗)

Collection of configuration settings for retrieving your registered images from their origin URLs. If your images come from multiple locations you will have multiple origin strategies. See the OriginStrategy topic.


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Customer|hydra:Collection|True|False|


```
/customers/{0}/originStrategies
```


### authServices (🔗)

Collection of IIIF Authentication services available for use with your images. The images are associated with the auth services via Roles. An AuthService is a means of acquirung a role.


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Customer|hydra:Collection|True|False|


```
/customers/{0}/authServices
```


### roles (🔗)

Collection of the available roles you can assign to your images. In order for a user to see an image, the user must have the role associated with the image, or one of them. Users interact with an AuthService to acquire a role or roles.


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Customer|hydra:Collection|True|False|


```
/customers/{0}/roles
```


### queue (🔗)

The Customer's view on the DLCS ingest queue. As well as allowing you to query the status of batches you have registered, you can POST new batches to the queue.


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Customer|vocab:Queue|True|False|


```
/customers/{0}/queue
```


### spaces (🔗)

Collection of all the Space resources associated with your customer. A space allows you to partition images, have different default roles and tags, etc. See the Space topic.


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Customer|hydra:Collection|True|False|


```
/customers/{0}/spaces
```

