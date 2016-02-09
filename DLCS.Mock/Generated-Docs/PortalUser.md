
# PortalUser

A user of the portal


```
/customers/{0}/portalUsers/{1}
```


## Supported operations


|Method|Label|Expects|Returns|Statuses|
|--|--|--|--|--|
|GET|Retrieve a Portal User| |vocab:PortalUser|200 OK, 404 Not found|
|PUT|create or replace a Portal User|vocab:PortalUser|vocab:PortalUser|200 OK, 201 Created Portal User, 404 Not found|
|PATCH|Update the supplied fields of the Portal User|vocab:PortalUser|vocab:PortalUser|205 Accepted Portal User, reset view, 400 Bad request, 404 Not found|
|DELETE|Delete the Portal User| |owl:Nothing|205 Accepted Portal User, reset view, 404 Not found|


## Supported properties


### email

The email address


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:PortalUser|xsd:string|False|False|


### created

Create date


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:PortalUser|xsd:dateTime|False|False|


### role

List of Role URIs that the user has


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:PortalUser|xsd:string|False|False|


### enabled

Whether the user can log in


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:PortalUser|xsd:boolean|False|False|

