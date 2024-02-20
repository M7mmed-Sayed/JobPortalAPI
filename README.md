# JobPortalAPI - .NET Core 7 Application

## System Description

The JobPortalAPI is a .NET Core 7 application designed to serve as the backend API for a job portal system. It provides functionalities for managing companies, users, job listings, applications, posts, and facilitates linking between companies and liked users.

## Website Structure

The API follows a RESTful architecture and is organized around the following main components:

- **Controllers:** Implementing CRUD operations for companies, users, applications, posts, and jobs.
- **Repositories:** Handling database interactions.
- **Models:** Defining data structures, including the Company, ApplicationUser, Application, Post, and Job models.
- **DTOs (Data Transfer Objects):** Structuring data for API requests and responses.

## System Requirements

To run the JobPortalAPI, ensure that you have the following prerequisites installed:

- [.NET Core SDK 7](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Entity Framework Core Tools](https://docs.microsoft.com/en-us/ef/core/cli/dotnet)


## Version 1.0.4 (Current)

### Overview

The initial versions include fundamental features for user management (signin/signup, email confirmation), company management (add, edit, remove, get), and application functionalities with JWT authentication.

### Features Implemented
1. **database and configurations**
    - create User,Company,Application , and job Models.
    - Create Response Factory.
    - create Error handling and ErrorList 
   
2. **User Management:**
    - Signin/Signup functionality.
    - Email confirmation for new users.
    - JWT authentication for secure access.
    - Further enhance user management with roles and permissions.(i didn't create roles till now)

3. **Company Management:**
    - CompanyController for handling CRUD operations on companies.
    - CompanyRepository for database interactions.
    - User property added to the Company model to establish a relationship.
4. **Job Management:**
   - JobController for handling CRUD operations on companies.
   - JobRepository for database interactions.
   - User property added to the Job model to establish a relationship.
5.  **Applications:**
    - Completed the implementation of application functionalities.
    - ApplicationRepository for database interactions.
    - ApplicationController for handling CRUD operations on applications.
    - get all Applications for specific job.

6. **Planned Features for Future Versions**

    - **Posts:**
        - Introduce a Post model for handling user-generated content related to jobs or companies.
        - Implement CRUD operations for posts.

    - **Linking Between Companies and Liked Users:**
        - Enhance company-user relationships by implementing features such as liking and following companies.

    - **Search and Filters:**
        - Implement search and filtering options for companies, job listings, and user-generated content.
    - **Authentication:** In future releases, we plan to implement authentication mechanisms to secure the application. All controller methods will be decorated with the `[Authorize]` attribute, ensuring that only authenticated users can access these endpoints.
    - **Documentation:**
        - Continue improving API documentation using tools like Swagger.
    - **Unit Test**




For inquiries or collaboration opportunities, feel free to reach out:

- **LinkedIn:** [LinkedIn Profile](https://www.linkedin.com/in/m7mmed-sayed)
- **Email:** mohamedsayed1167@gmail.com