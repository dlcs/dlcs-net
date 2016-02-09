
# Role

A role is used by the DLCS to enforce access control. Images have roles.The DLCS acquires a user's roles from a RoleProvider. In the case of the simple Clickthrough role, the DLCS can supply this role to the user, but in other scenarios the DLCS needs to acquire roles for the user from the customer's endpoints.


```
/customers/{0}/roles/{1}
```


## Supported operations


|Method|Label|Expects|Returns|Status|
|--|--|--|--|--|
|GET|Retrieve a Role| |vocab:Role| |
|PUT|create or replace a Role|vocab:Role|vocab:Role| |
|PATCH|Update the supplied fields of the Role|vocab:Role|vocab:Role|200 patched Role|
|DELETE|Delete the Role| |owl:Nothing| |


## Supported properties


### name

The role name - this might be the same as the ID?


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Role|xsd:string|False|False|


### label

Label for a slightly longer description of the role


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Role|xsd:string|False|False|


### aliases

If the DLCS acquires roles from the customer, they might have different names


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Role|xsd:string|False|False|


### authService (🔗)


```
/customers/{0}/roles/{1}/authService
```

The IIIF Auth Service for this role


|domain|range|readonly|writeonly|
|--|--|--|--|
|vocab:Role|vocab:AuthService|False|False|


|Method|Label|Expects|Returns|Status|
|--|--|--|--|--|
|GET|Retrieve a Auth Service| |vocab:Role| |
|PUT|create or replace a Auth Service|vocab:Role|vocab:Role| |
|PATCH|Update the supplied fields of the Auth Service|vocab:Role|vocab:Role|200 patched Auth Service|
|DELETE|Delete the Auth Service| |owl:Nothing| |

