# Final Project - SchoolAPI
##Course - Summer 2020 - IS690

### Developed By: 
### 1) Maulik V Patel (mvp46)
### 2) Jeremy Langenderfer (jl822)
### #) Jay Patel (jap256)
This API can perform the following operations:

### i) User Registration: Any user must be able to register through this page. Details like username, email id, password, first name, last name, phone number etc. should be captured.

![User Registration](https://github.com/maulikpatel1992/SchoolAPI/blob/Final_Snapshots/final%20project/1User_Registration.JPG?raw=true)

### ii) User Login: The registered users should be able to login through this page using proper credentials. Invalid login details should return an error with proper response code.
#### Login- Success for Authorise User
![User Login_Success](https://github.com/maulikpatel1992/SchoolAPI/blob/Final_Snapshots/final%20project/2UserLogin_Success.JPG?raw=true)
#### Login- Fail for Unauthorise User
![User Login_Fail](https://github.com/maulikpatel1992/SchoolAPI/blob/Final_Snapshots/final%20project/2UserLogin_Fail.JPG?raw=true)

### iii) Privilege escalation: This page should allow admin rights to the selected user.
#### We assigned one selected user an Admin rights. Now no new user can register as Admin.

![Privilege escalation](https://github.com/maulikpatel1992/SchoolAPI/blob/Final_Snapshots/final%20project/3_cant_register_as_admin.JPG?raw=true)

### iv) BREAD Courses: The user should be able to browse, read, edit, add and delete courses (BREAD). You can create separate pages where the user can browse all the available courses, read their description, edit them if required, add new courses and delete old courses.

#### Browse Course:
![Privilege escalation](https://github.com/maulikpatel1992/SchoolAPI/blob/Final_Snapshots/final%20project/4_Course_Browse.JPG?raw=true)

#### Read Course:
![Privilege escalation](https://github.com/maulikpatel1992/SchoolAPI/blob/Final_Snapshots/final%20project/4_Course_Read.JPG?raw=true)

#### Edit Course:
![Privilege escalation](https://github.com/maulikpatel1992/SchoolAPI/blob/Final_Snapshots/final%20project/4_Course_Edit.JPG?raw=true)

#### Add Course:
![Privilege escalation](https://github.com/maulikpatel1992/SchoolAPI/blob/Final_Snapshots/final%20project/4_Course_Add.JPG?raw=true)

#### Delete Course:
![Privilege escalation](https://github.com/maulikpatel1992/SchoolAPI/blob/Final_Snapshots/final%20project/4_Course_Delete.JPG?raw=true)

### v) BREAD Sections: The user should be able to perform the BREAD operations (as mentioned above) for different sections of the available courses. This page should also check if the relevant course exists or not while adding a section.

#### Browse Course Section:
![Privilege escalation](https://github.com/maulikpatel1992/SchoolAPI/blob/Final_Snapshots/final%20project/5_CourseSection_Browse.JPG?raw=true)

#### Read Course Section:
![Privilege escalation](https://github.com/maulikpatel1992/SchoolAPI/blob/Final_Snapshots/final%20project/4_Course_Read.JPG?raw=true)

#### Edit Course Section:
![Privilege escalation](https://github.com/maulikpatel1992/SchoolAPI/blob/Final_Snapshots/final%20project/5_CourseSection_Edit.JPG?raw=true)

#### Add Course Section: Success for Authorise User
![Privilege escalation](https://github.com/maulikpatel1992/SchoolAPI/blob/Final_Snapshots/final%20project/5_CourseSection_Add_AuthoriseUser.JPG?raw=true)

#### Add Course Section: Fail for Unauthorise User (Admin)
![Privilege escalation](https://github.com/maulikpatel1992/SchoolAPI/blob/Final_Snapshots/final%20project/5_CourseSection_Add_Unauthorise.JPG?raw=true)

#### Delete Course Section: Success for Authorise User (Admin)
![Privilege escalation](https://github.com/maulikpatel1992/SchoolAPI/blob/Final_Snapshots/final%20project/5_CourseSection_Delete_AuthoriseUser.JPG?raw=true)

### vi) Register for Sections: The user should be able to register for any section of any course. But check for the validity of the registration by checking if that course/section exists or not.

#### Register Course Section: By Any User (for this we use user with Student Role)
![Privilege escalation](https://github.com/maulikpatel1992/SchoolAPI/blob/Final_Snapshots/final%20project/6_CourseSection_Registration.JPG?raw=true)

#### Register Course Section When Course Section not exist
![Privilege escalation](https://github.com/maulikpatel1992/SchoolAPI/blob/Final_Snapshots/final%20project/6_CourseSection_Registration_CourseNA.JPG?raw=true)

### vii) Role Management: Only admins should be able to create new courses and sections. General users can only register for any course/section. Return the relevant response code if a general user tries to perform these actions.
#### We already shown above for Creating Courses and Section by Admin

#### Create Course Section : By user with Student Role
![Privilege escalation](https://github.com/maulikpatel1992/SchoolAPI/blob/Final_Snapshots/final%20project/7_UnauthoriseUser_CreateCourseSection.JPG?raw=true)

## Additional Features:

### viii) Caching:
#### We implemented caching into our API
![Privilege escalation](https://github.com/maulikpatel1992/SchoolAPI/blob/Final_Snapshots/final%20project/caching_User.JPG?raw=true)

### ix) Filtering
#### We implemented Filtering into our API
![Privilege escalation](https://github.com/maulikpatel1992/SchoolAPI/blob/Final_Snapshots/final%20project/Filtering_SectionEnrollment.png?raw=true)

### x) Searching
#### We implemented Searching into our API
![Privilege escalation](https://github.com/maulikpatel1992/SchoolAPI/blob/Final_Snapshots/final%20project/Searching_Course.png?raw=true)

### xi) Sorting
#### We implemented Sorting into our API
![Privilege escalation](https://github.com/maulikpatel1992/SchoolAPI/blob/Final_Snapshots/final%20project/Sorting_Courses.png?raw=true)

### xii) Paging
#### We implemented Paging into our API
![Privilege escalation](https://github.com/maulikpatel1992/SchoolAPI/blob/Final_Snapshots/final%20project/paging_SectionEnrollmentMgt.png?raw=true)
#### We implemented Sorting into our API
![Privilege escalation](https://github.com/maulikpatel1992/SchoolAPI/blob/Final_Snapshots/final%20project/Sorting_Courses.png?raw=true)

## Swagger API Snapshots:
![Privilege escalation](https://github.com/maulikpatel1992/SchoolAPI/blob/Final_Snapshots/final%20project/swagger/swagger1.JPG?raw=true)
![Privilege escalation](https://github.com/maulikpatel1992/SchoolAPI/blob/Final_Snapshots/final%20project/swagger/swagger2.JPG?raw=true)
![Privilege escalation](https://github.com/maulikpatel1992/SchoolAPI/blob/Final_Snapshots/final%20project/swagger/swagger3.JPG?raw=true)
![Privilege escalation](https://github.com/maulikpatel1992/SchoolAPI/blob/Final_Snapshots/final%20project/swagger/swagger4.JPG?raw=true)
![Privilege escalation](https://github.com/maulikpatel1992/SchoolAPI/blob/Final_Snapshots/final%20project/swagger/swagger5.JPG?raw=true)
![Privilege escalation](https://github.com/maulikpatel1992/SchoolAPI/blob/Final_Snapshots/final%20project/swagger/swagger6.JPG?raw=true)
