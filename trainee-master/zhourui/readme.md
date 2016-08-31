# Daily Report #

----------

### 03/06/2016 ###

1. Found and fixed 3 Getinge UI bugs with Jo's helping.
2. Research Asp.net MVC Global Exception Handler.
3. Implemented Getinge server-side exception handler and send to front-end function with Jo's helping.

----------

### 02/06/2016 ###

1. Created new branch named "FixKnownIssues\MissingSessionRedirectIssue" and fix redirect after missing session bug with Jo's helping.
2. Created new breach named "FixKnownIssues\RedirectAfterModifiedProjectIssue" and Fixing bug that user clicking "Data Management" after he created and modified some project data will redirect to index.
3. Fix database havn't primary key bug.

----------

### 01/06/2016 ###

1. Browsed Getinge website and found 9 bugs.
2. Debug PushService of butler at local and found a bug.
3. Viewed Getinge front-end source code.

----------

### 31/05/2016 ###

1. Viewed Getinge source codes.
2. Jo shown us butler rules and related things.
3. Created a windows service to loop access butler api named RefreshService.

----------

### 30/05/2016 ###

1. Viewed some visitor-app user stories.
2. Research visitor-app source code.
3. Continue configurating local butler issues.

----------

### 27/05/2016 ###

1. Debug butler in local, fixed local rooms getting issues.

----------

### 26/05/2016 ###

1. Finished deploy butler in AD or local, but there still some little problems (ExchangeService.GetRooms can't get room from Exchange).
2. Understand the butler's configuration and more details.

----------

### 25/05/2016 ###

1. Finished deploy butler to test AD environment with Jo's help (#30904 Try deploy the butler in local).
2. Understand the login process that Jo told to us.

----------

### 24/05/2016 ###

1. Practise using c# to operate Exchange (SendEmail, ReplyEmail, DeleteEmail, etc).
2. Research Bug#30395 and created an Excel file to compare client's ExchangeServer settings with ours, and found 16 different settings.
3. Kevin demonstrate Getinge to us both Web and IPad environments, Jo mentioned somewhere to notice.

----------

### 23/05/2016 ###

1. Finished configuring AD and Exchange environment, add team members to AD and Exchange.
2. Research Exchange Web Service API, known what EWS API is and what it can do.
3. Run Getinge.LCCApp and create a new project in Getinge to research calculate process.

----------

### 20/05/2016 ###

1. Setup the environment of AD and Exchange Server.
2. Viewed Getinge.LCCApp source codes.

----------

### 19/05/2016 ###

1. Learned something about AD and Exchange online.
2. Jo told me about Getinge.LCCApp project (architecture, functions, key points, etc). I know some about loading rate logic, because I participated an related project of logistics before that calcute truck loading rate.

----------

### 18/05/2016 ###

1. Tom and I resolved bug#30926 "Not send export file to", contact with Nils about conditions and details and finally sent email to Nils with solution.
2. Through this process, I known more about PowerPay what I didn't know before, something like schedule logic, file format, database fields and so on.
3. I help to contact with Nils, processing with Excel data, learn something new about PowerPay from Tom.

----------

### 17/05/2016 ###

1. PowerPay: Viewed some user stories of PowerPay on TP, known more details about PowerPay.
2. ADIT: Viewed recently commited codes of ADIT by Nils and Albinblent, and debug all mainly functions.

----------

### 16/05/2016 ###

1. Michael show me all about AssetManager (Architecture, ExceptionHandler, Authorization, Validation, etc).
2. Viewed PowerPay database structure and read over 50 user stories on TP.
3. Hide unlock icon instead of disable it (AssetManager US#30829).

----------

### 13/05/2016 ###

1. Found and fixed bug in US#30829 (in status edit view, ACTIVE column display abnormal).
2. Continue to familiar with AssetManager, viewed database structure, analysis it's business logic.
3. Tom told me about AssetManager's status business logic and some thing about history.

----------

### 12/05/2016 ###

1. Learned about AssetManager project from Tom, we discussed about US#30829.
2. Finished US#30829, modified all html files and js files, when user is ReadOnly, he can't see Add button and Action column (with edit and delete buttons).
3. Tested all pages with different roles, found some unchanged files then changed them all.
4. After I pulled new ADIT source codes and viewed it, I found there are already implemented ForgetPassword function, and it's similar with my opinion, so I learned how it implementation.
5. Today I felt it is more important to modify codes carefully, because I thought I was modified all codes of US#30829, but there are more pages in under level that I haven't noticed, when I viewed all pages I found it, so I will take more attention when I modify codes in the future.

----------

### 11/05/2016 ###

1. I was added to access PowerPay and ADIT projects, and I configured SourceTree and pull source codes.
2. PangSen explained PowerPay's logic to us.
3. Tom and I discussed about PowerPay and ADIT business logic.
4. Jack and Eric discussed with us about our feelings lately after we join the team.

----------

### 10/05/2016 ###

1. I tested reset password functions of ADIT, and found many problems, so I started to think about every process and rewrite. I added a MemoryCache to manage the Guid and the user who want to change his password and it will last for 1 hour, fixed send email function, created new API for validating Guid of user.
2. Try to know about TP and viewed some user stories of PowerPay project on TP .

----------

### 09/05/2016 ###

1. Today I try to replace DirectoryEntry with PrincipalContext, and made some demo to test, at last, I created a ANMO.Adit.Common project, and rewrote DirectoryAccessor with using PrincipalContext class, but not finished, tomorrow I will continue to code these.
2. Discuss with the team about last week's works.

----------

### 06/05/2016 ###

1. Today I started to refactor ADIT project, created a new project named 'ADHelper', created interface IDirectoryAccessor, and implemented it, includes GetDirectoryEntry(), AdPathExists(), IsUserExists(), RenameObject(), CreateUserAccount(), EnableUserAccount(), DisableUserAccount(), UnlockUserAccount(), ResetUserPassword(), ChangeUserPassword() methods.
2. I found a bug in refactoring which user be created at root directory that came out at yesterday when I modified method, so I add a config section named 'UserDN' in web.config, also added 'BadPasswordAttemptCount' section to scan times that user attempted to login before user change his password.
3. After that I switched some PrincipalContext to DirectoryEntry (Official recommended to use PrincipalContext, but now I'm not so familiar with it, and I can't implement all functions that DirectoryEntry can do), also reduced some codes and added some exception thrower.

----------

### 05/05/2016 ###

1. Today I arranged the user requirements of ADIT, finished all functions of user requirements (modified front-end and server-side codes), and finished unit test, then I reported the progress to Michael, he helped me find a logic bug which is the amout of special symbols in generating password.
2. After that, I opened ADIT、PowerPay and Previa projects at the same time to compare the implementation of AD accessor, because I preparing to refactor AD accessor of ADIT.

----------

### 04/05/2016 ###

1. In the morning, Beier sent the Previa project to me, and I started to build runtime environment, also asked Eric, but failed, at last Beier told me to run this project need to connect client environment via VPN and the client shut down our vpn account, then I browsed some user requirements, it seemed like a healthy report or related thing, because it can't be run normally, so I leave the codes temporary.
2. At afternoon, I continued to research the user requirement of ADIT project, generate password by AD security policy, and I finished it finally, and added a configuration section called NonAlphanumericCharacters in web.config to set the number of special characters in password, default is 2, and the result is right after I modified AD policy during website is running, I modified some other methods also.
3. At last I viewed the codes of Previa according to the relationships between these projects in the Previa solution.

----------

### 03/05/2016 ###

1. This morning, after meeting I started to improve the AD password policy tips of ADIT, added minPwdAge, maxPwdAge and pwdHistoryLength properties, and added related swedish informations, also modified return value of GetCurrentPolicy() method  to List<string>.
2. Af afternoon, I linkage tested front-end and server-end codes, added front-end tips when the password user inputed to change doesn't match AD password policies.

----------

### 29/04/2016 ###

1. This morning, I reviewed the service codes of PowerPay, try analysis and understood each function.
2. At afternoon, I researched front-end codes of ADIT, and found two fatal errors, when user clicked "forget password" and redirect to forget_password.html, there is only one input box to input email address, but in ko(Knockout), there're two objects were binded, one is Username, another is Email, and Email validated by password strength regular expression, so whatever email you input, the commit button will never enabled.

----------

### 28/04/2016 ###

1. This morning, after meeting I continued to research ADIT project, and learned some advanced operation of LDAP.
2. Af afternoon, I started to research PowerPay project, and record the main function of each module, also recorded the main function of each project, recorded technologies and difficulty.
3. For now, there's not any big problems of ADIT to me, after looking UserStory on PT, I'm sure I can start to refactor it immediately.
4. About PowerPay, I known much now, but not all details, so I need to look all UserStories on PT to confirm what I think is right or not, and ExcelTool and Service is the most important modules, I need more time to research it, I can get into it quickly.

----------

### 27/04/2016 ###

1. This morning, after meeting I continued to research ADIT project, and modified some bugs that was found yesterday.
2. After that, I started to look related content, such as LDAP, Knockout, Nancy, and did some practices about Knockout and Nancy.
3. Tomorrow, my plan is continue to practise Knockout and Nancy technologies, and create new Stage folder in Trainee folder to put my daily practices.

----------

### 26/04/2016 ###

1. This morning, after meeting I viewed PowerPay again, and modified User Guide.
2. Afternoon I started to know the source code of ADIT, and researched it, after whole afternoon's researching, I felt known the operation of LDAP, and I found 5 bugs, 2 fatal errors, the first one is the program cannot get any infomations from AD (PrincipalContext initialization is wrong) and when user click "Login" button without username and password will comes out an exception.
3. About the first error, I looked up many informations, and figured out the initialization of PrincipalContext and all kinds of various exceptions, finally I resolved this error, and 2 little bugs ran normally also.
4. Cause it's late when I fixed these errors, I haven't any time to fix the second bug, so I planned to fix it tomorrow, and refactor multi initializations of PrincipalContext, also reduce codes.

----------

### 25/04/2016 ###

1. This morning, at meeting I was asked to know about PowerPay and ADIT systems, so I continued to know PowerPay, and found two little bugs.
2. Afternoon our team demo the Welcome project, Taylor recorded the bugs in the demo proccess and sent to us, I have a impress about this project, it looks simple, but more codes behind. At last we known about new mission about TRR, and listened to the client's requirements.
3. For now, my main task is to know about PowerPay and ADIT projects, through these days learning, I already have my own idea about PowerPay, but don't know it's right of not, so when I get the account, I will login PT to know UserStory about PowerPay project, and ask questions to Sen Pang.
4. My plan for tomorrow is to know about ADIT project, get the requirements first, then view the demo and codes.

----------

### 23/04/2016 ###

1. Today is the last day of EPiServer training, in the morning Linda taught us about Find of EPiServer, I should register a new account and get a developer key, then integrated with EPiServer, and Linda taught us about using and developing of Add-on.
2. Afternoon she taught us something about Globalization, and multisites and settings, after that , she taught about Security, how to avoid the end user access some key values through url.
3. At last, Houde Zuo passed the verification of EPiServer, we're all happy for him.
4. Though this week training, I've clearly known about the using of EPiServer, and known about the simple developing about EPiServer (PageType, BlockType, MediaType), but I need more exercises about advanced development to improve my abilities.

----------

### 22/04/2016 ###

1. Today is the fourth day of EPiServer training, the advanced training is began, at first Linda taught us about Log4Net of EPiServer, then she created and implemented Comment and some related functions.
2. Afternoon she taught us something about AdminUI customization, because these're advanced courses, I felt cannot catch up with the progress, what she taught I could understand at that time, but it's difficult to remember, maybe because I havn't a clearly concept about the whole EPiServer system.
3. After that, I did exercises A1, B1-B7, and finished each exercise step by step, though I could understood a little thing when I copy code, but more courses I couldn't understood.
4. So in the evening, I started over the exercises of basic Module A&B, and felt more deep understood than first time, and I have much time, so I thought about somewhere I couldn't understood before.

----------

### 21/04/2016 ###

1. Today is the third day of EPiServer training, in the morning, Linda taught us how to create Content Area, Partial template and Channels, also taught us how to create Block and Media, after that we did some exercises, and I finished exercises C1-C4, D1-D4.
2. Afternoon we learned how to create Menus and Listings, also learned Search and Filter, at last Linda taught us the architecture of EPiServer and the initialization proccess of the EPiServer system.
3. After that, I did exercises E1-E2, in the evening we were trained AngularJS, and I felt learned something, like how to improve the AngularJS application performance, and some principles of AngularJS.

----------

### 20/04/2016 ###

1. Today is the second day of EPiServer training, in the morning, Linda taught us how to configure MembershipProvider and RoleProvider in Web.config file, and she created NewsEditor and NewsPublisher users shown us the differences.
2. Afternoon we learned and exercised about Exercises B1-B6, we learned how to create layout with bootstrap, how to create base class of Page Type and Controller and View Model etc, and I finished the exercises according to the course.
3. I met a little problems when I do Exercises B2, because I closed div tag so early, at last I fixed it under Eric's helping.

----------

### 19/04/2016 ###

1. Today we started training EPiServer, at first, Linda taught us the structure and principle of EPiServer, then she shown us how to create project, how to update, and how to operate EPiServer (create and edit Page,Block etc, ACL, Schedule).
2. Afternoon we learned and exercised about Exercises A1-A4, known how to create, update and merge EPiServer database, also created Page, Controller and View by myself, and created new website in EPiServer website.
3. I met some problems, but I did it before, so I resolved it quickly.
4. In the evening, I do some preparations of the courses which will be teach tomorrow.

----------

### 18/04/2016 ###

1. After morning meeting, Jo introduced new requirements of project "ADIT" to us, the client need to invite external user to use the system, so we need send a URL to them, through it, they can modify their password on the specific page, we add the user to AD domain, and we need add "forget password" function too.
2. At the same time when we known about "ADIT", Jo introduced the framework of the project (internal, external, application etc), using technologies (LDAP, knockout, nancy etc), and I learned LDAP and nancy principle.
3. After that, I started to learn EPiServer, read documents at the offcial website, learned about the structure, PageType, BlockType etc, and make a simple page excise, had a basic known about EPiServer's structure and function.
4. Finally I made a excise referenced "How to do it".

----------

### 14/04/2016 ###

1. This morning I started to write user guide of PowerPay, introduced functions of each page.
2. By 2:00 PM, I finished most introductions, but there're more three major functions were not introduced, for now, the user guide is not perfect, I will improve it little by little.
3. After that, I started to learn EPiServer, build the environment, and successfully ran it, then I browsed some pages of the admin pages.
4. Later, I viewed each admin page, and had a basic known about this CMS system, also read some documents on EPiServer official site.

----------

### 13/04/2016 ###

1. This morning, I attended the meeting of maintenance group, the team discussed about refactor of Welcome project, then Jo assigned tasks to each one.
2. After that Jo let me researched UTC problem with Taylor, because Taylor learned the project yesterday, so I assisted him.
3. Then Tom told me to setup a new version of SQL Server on the server, so I unistalled version 2012 and setup version 2014.
4. I found server system (windows server 2012 r2 datacenter) was not actived, so I downloaded some activator, maybe some one had virus, the server setup some bad software automatic, then I had to fix, it took a long time, at last I fixed it.
5. At last, Taylor and I resolved UTC problems, first get client's UTC time by using toUTCString method, then remove string "GMT" and save it to DB, when geting data, We got the date, and substract the offset of locale timezone, then shown to end user, we tested some times, and all success.

----------

### 12/04/2016 ###

1. This morning, I attended the meeting of maintenance group, the team discussed about Pitch and Welcome projects, then Jo assigned tasks to each one.
2. After that I continued to research PowerPay project, for now, I have a rough impression of the project, I read all User Stories that Tom sent to me, and browsed every web page, but the Swedish is hard to find translation.
3. Then I built the localhost enviroment of PowerPay, I built it all by myself, so I felt more understood about the project structure.
4. At afternoon I tried to understand the web pages, find translations of every page's Swedish's word in Bing website, tried to understand the functions of the project, but there're some questions, then I noted them.
5. Later, I researched AngularJS of PowerPay, and swiched the language to English, felt more deepen understood of this project's functions.

----------

### 11/04/2016 ###

1. In the morning, I started to learn about the PowerPay project, at first Tom G told me some about the project structure, then he let me to deploy the project to IIS server.
2. At afternoon, I deployed the project, adjusted the web.config settings of Web,SSO and WebAPI, and it came out some questions, but I solved them, finally succeed deployed.
3. Because the server not allowed multi users login to the server at same time, so I configured it, now 10 users could login at the same time, I added group members infomations to AD and Exchange, and send their account to them.
4. Then Tom G send the requirements document of PowerPay to me, but I haven't time to read, In the evening I will read it for some.
5. In the evening, I read some requirements document, the files Tom G gave to me seemed not all, but I known about the format and content about User Story through it.

----------

### 08/04/2016 ###

1. Today I continued modified the Repository layer that was implemented by NHibernate, and restored the UnitOfWork which I deleted before, also fixed problems of unit test, but two test methods still had some problems, this weekend I will fixed it.
2. During these operations, I met some problems, like as "There is already an open DataReader associated with this Connection which must be closed first", at first I added "MultipleActiveResultSets=True" to the ConnectionString, but there're another problems, after that, I found there's a ToList() in LoadAll() method of Repository, and in Logic layer there's a another ToList() method, I thought the problem came out from here, then I remove ToList() method in Logic layer, and the program run normally.
3. After that I found there's bug in front-end, judge of the presentation parameter of browser's url, "True" or "False", I treated it as bool value before, and now I judge it as string, and project run normally.

----------

### 07/04/2016 ###

1. Today I removed all xml config file of NHibernate, implemented by codes.
2. After yesterday modified, some unit test's methods were error, and I fixed it today.
3. At noon, Eric trained us the usage of Castle Windsor, and discussed with us, answered our questions in using it.
4. At afternoon, I practised to implement the unit test of Repository, and successfully tested, after that, Alex trained us Unit Test, about theory of unit test and practise, and modified my project to show us how to test Logic layer rightly.
5. In the evening, we were trained the basic of NuGet, how to operate NuGet with PS, and how to create a local or remote NuGet repository .

----------

### 06/04/2016 ###

1. Today I was refactoring project PlanPoker, modified EntityFramework structure to NHibernate structure, and successfully finished, but the codes are not clean, tomorrow I will refactor it.
2. At first, I modified Data layer, declared model properties as virtual, created xml file for mapping models.
3. Then, modified many methods in Repository layer, created DbFactory to manage Session, and Implemented it.
4. At last, modified some codes in Logic layer, call methods of Repository layer, finished the original functions.
5. There's no changes in WebAPI, only add a hibernate.cfg.xml config file. Also found a bug that left at last change, then I fixed it.

----------

### 05/04/2016 ###

1. Today I modified the problems that found out at last friday code review.
2. Add Status property in Model, checking Status at frontend side if success or not.
3. moved Header tags to each page, removed show menu of not in check route.
4. Removed #region and #endregion tags in Test projects.
5. Replaced HttpGet with HttpPost in Login of WebAPI, and validating password at WebAPI side when user modified his profile, improved security.
6. For now, the security problem in login are not finished, and I took some time to learn about Session related knowledges.
7. At last, I learned some knowledges of NHibernate.

----------

### 01/04/2016 ###

1. Today morning I continued learning Entity Framework, and found some problems when pratising, between two objects, there is 1:N relationship and N:N relationship too, the automatic created DB will be mess, I searched solution online, and found InverseProperty seems could solve this problem, but I didn't test yet.
2. I learned the usage of Key, Required, Length, NotMapped, ComplexType, ConcurrencyCheck, TimeStamp, Table&Column, DatabaseGenerated, InverseProperty&ForeignKey attributes.
3. Afternoon, before Code Review, I cleaned up my code again, move some methods into planPokerApp.js file, reduced codes in controller.js file, made codes more clean.
4. Before COB, Jack, Eric and us made a code review, and found some old problems again, also found some new problems, I compared my codes, the problems not so much, but still need some improve.

----------

### 31/03/2016 ###

1. Today morning, I learned EF knowledges at MSDN online, also learned some related knowledges, and studied some practices, but some knowledges I haven't understood.
2. At afternoon I started to test EF in PlanPoker project, for keep original project unchange, I created Stage-5, at first I modified profile_template.html, user can see owned projects and related projects, also can create new project in it, user can add owner and related users in new project window, through checkbox list, determin related(invited) users in this project, but not finished all functions yet.
3. I found some problems at this time, but I resolved it soon, in the evening I created a Users_Projects entity, for saving relationship between User and Project, and tested success, this pratise is just for more familiar of EF, and pratise the things which learned from "Clean Code".
4. When I starting these changes, I modified on Stage-4 at first, and then I thought Jack and Eric will review our codes tomorrow, so I copied the codes to Stage-5, and discarded the changes, caused the changes hard to be found between stage-5 and stage-4.

----------

### 30/03/2016 ###

1. Today I spent all this morning to read the book "Clean Code", from capter 4 to capter 10, though I was browsed, but I remember some important rules.
2. At afternoon I continued to read this book, then thinking of my "PlanPoker" project, refactored some codes, according to SRP, I refactored Insert method of EstimateController, and separated two private method, there are AddToMemoryCache and UpdateMemoryCache, these mothods' responsibility are single, function are clear, and made Insert method more readable.
3. About four o'clock afternoon, I read the "unit test" book, this time I understood more deepen than first read, emphasized read some important parts, and understood some where I didn't understood before.

----------

### 29/03/2016 ###

1. Today morning, I cleaned up my codes again, modified edit logic of UserLogic.cs，removed check if user email exists method, only check if user new email exists, and moved some controler.js files to each function folder in partials folder, renamed html files in partials folder and added template suffix, modified parameters in js files (ret->returnObject, err->errorMessage), also modified parameters in other layers' lambda expressions (u->user).
2. At afternoon, I read the book named "Clean Code", for now I finished read the first 3 chapters, felt learned a lot, I agree with the author, and commented someplaces, I felt the book is good, and I'll read it more times later. After I read the whole book, I'll refactor my project again.
3. After I read the book today, My feeling is, the most important thing is to form good habits, naturally write meaningful naming, Single Responsibility functions and so on, make code reader felt read your codes just like read a story is the ultimate purpose.

----------

### 28/03/2016 ###

1. Today I fixed all remained problems, and arranged codes, make codes more readable, more shorter. all services file combined to one, multi line reduced to one, cleaned all redundant codes.
2. In the morning, I arranged frontend codes, combined all services files to one, renamed main.js to planPokerApp.js, and modified every js file's structure and name.
3. At afternoon, at first I arranged codes of WebAPI and Logic layers, with using ReSharper tool, changed names of methods and variables,  optimized codes.
4. After that, I modified frontend again, fixed the problem which Eric mentioned was when user selected a project in Estimate page, after page redirected, project name can't be bind. Also fixed some other bugs.

----------

### 25/03/2016 ###

1. Today after I finished all rest unit tests, I researched how to test in data layer, but failed, only finished DatabaseFactory unit testing.
2. In the morning, I finished Logic and WebAPI unit tests, and make some methods more easy, codes are more readable.
3. At afternoon, after I learned Guideline I used it to inprove my codes.
4. before COB, Jack, Eric and us made a code review, and found many problems, we recorded, and tomorrow I will send to the team, if I have any time, I will make a code review this weekend to these problems, reasonable naming, code readable, layer's responsibility reasonable.

----------

### 24/03/2016 ###

1. Today I refactored functions when I writing unit test, and modified model converter that not finished yesterday, also add email validating funciton.
2. In the morning, I finished the problems that yesterday not fixed, and finished unit test of WebAPI.
3. At afternoon, after Eric guided and discussed with ZhangYi, I learned how to write unit test of Logic layer, then I finished UserLogic unit testing, and test various possibility in every function unit testing.
4. I was going to finished all unit test for all layers, but I met various problems, so it took me some time, but I learned how to wrote unit test, so coding will be faster from now.
5. In the evening I learned Guideline lesson by Jack training, and known more important things when coding, and remembered some, I suggest to sent the training PPT to us, so that we can learned it in coding, to make a good habit at first.
6. Tomorrow my plan is to finish the rest unit test, and think about new features in the weekend.

----------

### 23/03/2016 ####

1. Today morning I refactored some module of PlanPoker project, every layer has own model, but my implement seemed not right, but run success, maybe model changed few, I will researched how to do is right later.
2. At afternoon I continued to write unit test of controller, and finished most of controllers', and most test successful, except EstimateController, maybe the implement of EstimateController not prefect, and I modified other's problems.
3. In the evening, I finished all controller's unit test, but in this progress I always felt couldn't continue to write, according to Eric said, when you can not continue to write, the implement of the function maybe not prefect.
4. Tomorrow, my plan is finished all layers unit test, at last I will handle all problems together, then modified function or unit test.

----------

### 22/03/2016 ###

1. Today morning I modified problems that Eric mentioned yesterday in code review, I splited one Service and one Controller to multiple files, and created folders to place them, then the code are clean and readable.
2. At afternoon, I started to practise the unit test coding, after study all afternoon, I knew more about principle, but didn't know how to write, and felt confused about coding in various situation.
3. In the evening, I studied unit test samples online, and made some progress, also knew how to write with coding some, I thought at first I need mock function's input, then execute the function once, at last, determine the value are same as the assert.
4. Tomorrow, my plan is to test some complex functions using unit test, so I can quickly familiar how to write with unit test.

----------

### 21/03/2016 ###

1. Today morning I continued improve the project "PlanPoker", add tips for creating project, and check password consistency, moved style in HTML file to site.css, after that I tested project functions, and all passed.
2. At afternoon, I started to study UnitTest technology, and known a little, but I should think more about how to coding more comprehensive test.
3. After that, Eric and us made a Code Review, everyone shown his own project, and reviewed the code, I recorded every question that Eric metioned in this proccess, later l'll send it to the team.

----------

### 18/03/2016 ###

1. Today I continued fixing loop bugs in estimate, through using global $rootScope and inject $interval to control start and stop of timer, alse fixed all problems that I known.
2. In the morning, I finished redirect function when big screen click Next button, and determine the header show or hide in different page when route redirecting.
3. during this period, I met some $interval problems, that after user has been kicked out to dashboard page, then user clicking logout, the loop not stoped, and I fixed it later.
4. At afternoon, I deleted some useless html and js files, and deleted all comments, also added error tips that haven't before.
5. When I tested my PlanPoker project, I found a big bug that when big screen click Next button, In estimateController, deleteEstimate function should delete $routeParams.projectId, not the proejctId in cookie, then I fixed it.
6. Now PlanPoker AngularJS edition already finished, the project was recreated, all the codes were rewrote, implement SPA, without jQuery, tested all functions success, except some tips not implement.
7. This weekend I will review my PlanPoker project codes, and fixed all bugs that I found, also prepare lessons of unit test.

----------

### 17/03/2016 ###

1. Today I continued to implement Estimate functions, basically finished all most important functions, but there're some new problems came out, those are little bugs.
2. In the morning, I modified estimateController, and finished loop function for Estimate, because of my mistake of using $interval, caused error in console, but it not effected functions, and I fixed it afternoon.
3. Afternoon, I improved estimateController, finished dynamic show estimate value both big screen and client screen, and improved estimate page, added project name selector, after user selected the project the page will redirect to estimate page of selected project.
4. Now, the main functions are all implemented, all codes were rewrote, and functions implemented without jQuery, excepted using of bootstrap, but there're some details need to improve, like tips, and redirect functions when big screen click Next, but user's screen not redirect, if I have any time this evening, I'll proccess these details, so I think I can finish all functions before this weekend .
5. In the evening, I fixed create, delete, update functions bugs of project, also fixed estimate redirect address after big screen clicking next button.

----------

### 16/03/2016 ###

1. Today I started to implement Estimate functions, and add Project Manage function, also fixed some problems in Dashboard.
2. In the morning, I modified Service, Controller of Estimate, and implement user image show up when user selected file.
3. Afternoon, I reviewed the logic of orginal Estimate.js file, then I started to modify, now the polling of Estimate page is worked.
4. before off duty, the redirect function of Dashboard to Estimate was not finished, but I finished config of Route, maybe the config was wrong, or get value was wrong, I'll research later.
5. At evening, I researched angular-ui-router.js, and tested to use $state but failed, so I reset all my changes, after that I fixed a bug of saving user image in profile, and fixed the redirect function of Dashboard to Estimate(because I wrote $location.path="" which is wrong, the right code is $location.path("")).

----------

### 15/03/2016 ###

1. After yesterday's AngularJS traning, I felt more understood about AngularJS, and I finished Route, Cookie functions implement, I tested ui-bootstrap, but failed, finaly I implemented dropdown of header by using jQuery framework.
2. In the morning I finished cookie set & get functions, I can operate cookie basically now, but I tested failed when using $cookieStore.putOjbect, I didn't know why, so I used $cookieStore.put to implement instead.
3. Afternoon I implemented the ng-controller of dashboard, and reduced a lot codes in HTML by creating pokers object array, dynamic load poker image by using ng-repeat.
4. After that I researched image loading function in profile, and succeed, but I found there's some problems when using ng-src to bind, so I used a function to bind, it works.

----------

### 14/03/2016 ###

1. Today I continued Modofied PlanPoker.Web.AngularJS, Changed Directive mode to Route mode, and implemented redirect function after user loged in.
2. And I added profile, dashboard, estimate pages, tested Route mode functions.
3. All afternoon I was testing binding of select tag in dashboard page, but failed, I will try later.
4. At last, I attend traning of basic of AngularJS, I could understood most of the training knowledges, but not familiar when using, so I need more pratise, the teacher's teach so fast, but traning content is rich, every AngularJS basic knowledge are in it, and he show us some actual cases, so the training made me learned a lot.
5. In the evening, I continued check data binding problems of select tag, and finally tested success, one is rookie mistake, another is the return value is an array, so get the data need use the method which when accessing the array.

----------

### 11/03/2016 ###

1. Today morning I continued modifing PlanPoker with AngularJS, and I finished input validate function, after that I was stucked when operate DOM using AngularJS, I felt helpless and didn't know how to ask.
2. Just for this reason, I felt there's distance to understanding of AngularJS, so I started some basical pratices, I understood how to operate object, when ng-modal attributes are in HTML, and model created in Controller, that I shouldn't to modified value binding manual.
3. Afternoon I created a User.html page, finished CRUD function of User object, and tested successfully, I thought familiar to operate an object, and the entire proccess I haven't check any articles, but I'm confused in operating DOM.
4. Now, I basically understand about Service, Controller, Model binding, at weekend I will learning Directive of AngularJS (the Directive seemed to be operate DOM), I will try my best to use Directive before next monday, lay a basis for AngularJS training next monday.

----------

### 10/03/2016 ###

1. Today I continued learning AngularJS, after I practised two online examples, I started to modify PlanPoker project, and browserd some practices online.
2. When using AngularJS, I divided it three classes, at first I created ng-model.js to define global app, then created userService.js to interactive with server-side, at last I create userController.js for controlling views, but I didn't know how many functions it can do and I don't know how to use it, so I felt it's so hard to me to implement the functions with AngularJS which was implemented with jQuery.
3. Afternoon I modified Login.html, Register.html, and it could interactive with server-side basically, but I didn't finished, I can't implement when clicking img tag to trigger input file tag, so the img value couldn't be set, but it can save user to database without image, and user login can use now.
4. For specific information to front-end, I found WebAPI's Controller's return value has some problems, so I modified return value to HttpResponseMessage of UserController, then it can transfer 'OK', 'BadRequest' informations.
5. In the evening, I didn't changed the PlanPoker project, learned a few AngularJS articles online again.

----------

### 09/03/2016 ###

1. Today morning, at daily meeting, Eric assigned us to finished fixing left bugs yesterday, I fixed header navigator, using .nav class in bootstrap, and modified css style, at the same time, I think about the right menu show or not in different page.
2. After we tested fine, we merged code and pushed, there's no conffict this time, maybe the files we modified are different.
3. At noon, I found one problem in my code, there's no success infomation show up after user successful modified his own profile, so I added a modal window, after tested I commited and pushed.
4. All afternoon, I was learning AngularJS, at first I wish to modified code at orginal project, but it's not so easy to me, maybe I'm not clearly knowing about AngularJS proccessing, I found an article online, according to it, I created some page to practise step by step.
5. This evening, I did some tests about AngularJS, and felt a little comprehend, but there's far away from using AngularJS as I wishes.

----------

### 08/03/2016 ###

1. Today morning, our PlanPoker came some problems when using, after clearly problems, Eric assigned us tasks, I was in charged of the Profile proccess.
2. My task was easy, I created new Profile.html page, added username, password, pricture controls, to get user's infomation and binded to the controls, after confirm the old password which user inputed was correct, call Update function in UserController, to finish the update function.
3. Afternoon, we be asked to use browser's cookie, I viewed relative articles, and tested, it could finished function what I want, but this work assigned to Lizhen and Zhangyi, I viewed their code, their code were writen in common.js, it's prefect, I didn't considered then.
4. After worktime, Eric reviewed our code, felt there're many problems, and offer a logical proposal, and help us to modified code to mid-night, I felt so sorry about our coding ability, made Eric wasted much time to help us.
5. In the evening, I browsed the article "Add Request Headers in AJAX HTTP POST using jQuery", and remembered how to use, not tested, but I've added to my browser favorite.

----------

### 07/03/2016 ###

1. Today morning, I modified my code according to you mentioned, One program function implement one actual function, don't implement logic processing in Controller .
2. After the team commited and pushed the code, I started to learn AngularJS, read more, test few, and browsed many articles, I understood the processing and principle of AngularJS.
3. Through today learning, I felt I'm not familiar with AngularJS, I can understood the code, but when I coding, I don't know how, so I plan to practise tomorrow, and try to modify and test some easy part of PlanPoker.
4. At evening, I learned something of web developing, like WebRequest, ASP.net work principle, ASP.net keep user state etc. but only simply, I should read more basic articles in future.

----------

### 04/03/2016 ###

1. After today daily meeting, I reviewed my code, and compressed my js file and HTML file.
2. Then I started learning about coding guideline, and browsed js,webapi,c# articles roughly, and thinking about it, I don't understood somewhere so clear, I will read it more times later.
3. This period I learned about Git command line usage, and I understood Git suddenly after short relax which I can't figured out before.
4. Around 3 o'clock afternoon, I started learning AngularJS, according to examples online to practise, it's so powerful, I should pratise more. I couldn't understood the 'StudentManagement' project which using AngularJS at that time, after I learned AngularJS tonight, I can understood some.
5. At 5 o'clock afternoon, Eric organized us to review our PlanPoker code, let us more deepen in touch with others' code and taught us how to do in a real project, how to do better, where is not right, make us deepen known about our own code.
6. I planed to read others' code in PlanPoker this weekend, more familiar with the entire project implement.
7. Sorry about sent the daily report so late, because I rent room these days.

----------

### 03/03/2016 ###

1. Today morning after the team show the feature of the Plan Poker, he commanded us to improve the product feature more perfect, there are 6 point, then the team devided the work, I charged Register, Login pages.
2. In the morning, I finished validating feature quickly, but pages are heavy, then I used jQuery.validating.js, after I figured out how to use it, I finished validating feature in other way, and js file are clean.
3. In the afternoon I started the feature of upload user image, under your guiding, I clean much interactive between server-side and front-end, after user selected img file, the image will show in img tag soon, when creating use, save image Base64 coding to database.
4. In the Evening, we all learn about Scrum training of Jack Wang's, I learned so much, and I will read more articles and books of Agile. But now I need improve my ability quickly more important, I will pratise the training in the future working.
5. After go home, I merged validating of image tyep and size in my branch which coding by Jiangbo Qu, And I added password strength checking, finished validating of Login page. Now my develop speed is slow, largely because I'm not familiarity with front-end developing, it took me much time, in the future I'll consult other in time, avoid waste much time at problems that I can't resolve quickly.

----------

### 02/03/2016 ###

1. This morning I finished code arrangement, and add some functions in Repository, Logic and Controller Layer.
2. At noon, when we merging, there came out many problems, the team discussed and resolved, modified some server-side functions.
3. Afternoon we found there were not user picture upload function, and I starting to implement it with using uploadify.js, I searched many articles online, after long time debuging, finally tested succeed, after that we add UploadFileHandler.ashx, it can recieve requests of front-end, and it were tested.
4. Though there were some problems, I modified it at evening, modified bugs in user register, uploaded file path can be saved in database, and file be saved in upload folder in server-side.

----------

### 01/03/2016 ###

1. Today I continued the "PlanPoker" project, this morning, I finished Modal window of Register and Login, And implemented js validating, specially avoided my problems that you mentioned few days ago.
2. After pages completed, I finished functions of Controllers, through debug, finally finished the interactive between Pages and WebAPI.
3. After the team discussed the implemention of the Estimage page, we had a basically plan, that we create a Dictionary in Controller, save uses selected poker at Project page, and auto polling in Estimate page, but there are problems when I implementing it, Jack Wang told us it will create new object per time when user request the Controller, taught us to manage it in CacheManager, finally we finished all codes based on Jack Wang's and your's, tested and achieved the requirement at front-end.
4. After today pratise, I found I'm weak at interactive of pages, didn't know create a simple variable in Controller can't achieve our requirement, knowning few about CacheManager, didn't know how to use it. But I felt the power of the team, which you do not know but others do, discuss and learn to each other can improve your ability quickly, I will share what I know to the team, and improve with the team quickly, to cover the shortage of personal.

----------

### 29/02/2016 ###
1. This morning I did some work of Stage-2, created a model in v2 of Stage-2, but not finished because Jack Wang assigned we new practise.
2. The task called PlanPoker, Jack Wang demanded new requirements to us, explained clearly, and taught us how to evaluate develop time with Fibonacci, I learned how to analysis requirement, how team work together, how to assign work, and team power.
3. After the team understood the requirement clearly, we discussed requirement again and assigned works to everyone, I was assigned to write the framework, which work I planed took 1.5 days, but now I felt not enough, because I took long time to debug a problem with missing a reference, I will catch the progress tomorrow.
4. I'm beginner of using webapi, and the task came so fast, I took a quick glance few articles online, and understood base principle, I felt it's same as webservice, but I couldn't tell where, there're not big difference when accessing their address.
5. Before today's off-duty, I run the framework successfully, but only implemented Create function of UserController, I planed to add others tomorrow's morning, after done, I will cooperation with team at details of implement front-end.

----------

### 26/02/2016 ###
1. This morning I started work on v2 of Stage-2, at first I optimized all pages, add Dashboard page, remove redundant tags in HTML, optimized CSS contents. Certainly there were some insufficiency, I will review my code ofter in the future, and thinking about prioritization scheme.
2. Though the presentations were not totally matched with mockups requirements, but through these days exercises again, my developing are more skillful and quickly.
3. After I finished all pages, I started to add modal pages and JS validating function, All the modal pages were finished with a little problems, and the editing page not finished yet, when I want to catch the table data, I was confused of using JS script, after I referenced some article online, I understood in general, will try this weekend.
4. In process of writing JS validating function, at first I validated every page in every JS function, then I found functions amount will be large along with pages amount, it's difficult to develop in future, so I validated all input controls in one JS function, by checking type attribute of input control with switch statement, basically implemented validating all pages in one JS function, decreased the workload, though the function was not perfect, but I felt improve a lot.

----------

### 25/02/2016 ###
1. Aftrer today morning meeting, I spent entire worktime to layout webpages according to requirements of mockup, for using HTML,Bootstrap more skillful.
2. I spent few time to study load function of jQuery, and successfully loaded Header and Navigator partial page, but breaked at using <a> tag, when using it to process dynamic load partial page by click functiuon, I have no idea how to do, after try and failure many times, I chaged to design every page, finally finished all layout and link of pages, but Modal and JS not finished yet, I plan to start it tomorrow.
3. At the begining of designing pages, I made HTML more simply, but CSS is so heavy and mess, difficult to find place where need to be modified, Afternoon I spent some time to research it, then I come up with CSS reusable of Jack's training,so I redesigned every module of page, defined style uniformity as possible as I could, then I could use a few CSS control more HTML tags.
4. The implement of dashboard was not good, I plan to review my all pages HTML and CSS again after I finished designing Modal and JS, make HTML and CSS readable and easy to modify.
5. After worktime, I attended the training of English every Thursday, the training was interesting, everyone took part in it, I felt so effective. last time training is develop of Android, because of my weak baisc, I was not attended it.

----------

### 24/02/2016 ###
1. This morning I modified project models according to mockups requirements, and improved some Controller, but Views changed few.
2. Because I used in-built authorize function of MVC which created earlier, some bugs came out and took me much time to fix it, and I found some MVC authorize articles online, knowing more about authorize process(like override AuthorizeCore and OnAuthorization, but not so cle
ar).
3. Then I Created BugType,Attachment models, and implemente Repository,Logic classes, soon after I realized it doesn't make any sense, it can be handle in BugView directly.
4. After listened HTML and CSS training by Jack Wang, I understood HTML,CSS more deeply, and I will try my best to avoid these mistakes, make HTML more easier and readable, make CSS naming more standardization, though the content of training were easy, but I was impressed by it, and it was what I need. I feel sorry about forgot to add this paragraph, because my thinking was filled with user authorizing today.
5. Afternoon, I modified UserController, understood user login process roughly, and implemented auto redirect to Dashboard page after logged in, because there're other views not finished yet, need add user record to database manual temporary.

----------

### 23/02/2016 ###
1. This morning I fixed bugs that I found yesterday(not creating DB by itselft), I found it were Logic Installer not added in using Castle Windsor(because the bug messages are so general, so it spent some time), after fixed that bug, I found new bug in wrong relationships of models, when fixed it, program run correctly.
2. After I fixed those bugs, I created Bug,Login,Register,Developer,Project,Dashboard Views, but only created layout, it took me much time
3. At last, I spent few time to finish CRUD of Project,Developer,Bug Controller coding, it's easy to call function of Logic Class, and using model as parameter in CRU, preparing for translating data in MVC and jQuery.
