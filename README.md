# Description:
- An Instagram-like web application with very basic functionalities.
# Feautures:
- Registration requires email confirmation.
- Users are able to follow other users.
- Users can be followed.
- Profile can be private. If profile is private, other users are unable to see what has been posted, until the private profile approves their follow request.
- User can edit their bio.
- Users can create posts.
- Users can like posts of other users.
- Users can comment on posts.
- Users can remove their own comments.
- Creator of the post can remove any comment.
- Creator of the post can delete it.
- Users can search for other users by their username.
- Admin can view all users and ban whoever he wants.
- Feed with all the posts of the users you follow.
- Real-Time one-on-one chat. No group chats yet.
- Real-Time notifications. You get a notification when user follows you, requests to follow you, comments on your post or likes your post.
- You can upload both image and video posts.
- You can preview your post before uploading it, to make sure you don't upload something else.
# Important!:
This is how your appsettings should look - https://pastebin.com/HLYTdLLq .
The application is configured to not store anything when a record in the database is deleted! You need to set ON DELETE CASCADE using SSMS on foreign keys. For example a Post has Comments, Likes and Notifications. You need to edit the foreign keys on those 3 entities and set them to cascade. The top answer on this SO post explains how to do it pretty well https://stackoverflow.com/questions/6260688/how-do-i-use-cascade-delete-with-sql-server .
# Stack:
- Back-end Framework - .NET CORE WEB API.
- Front-end Framework - REACT.
# External services:
- The application uses cloudinary for video and image storage.
- The application uses SendGrid for email confirmations.
# Design:
- UI is based on the standard asp.net core template and is mostly self-made, with some components coming from the web :) . Please excuse my poor css skills :( .
# Preview it yourself:
- The app is currently being seeded with sample data, you can follow users p_holley, l_carter, r_nelson, r_matta, j_acosta by searching them, once you've registered. They'll have a couple of sample posts.
