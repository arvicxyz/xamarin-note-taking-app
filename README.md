# Introduction

A note taking app test made using Xamarin.Forms.

# Development

This project is made using Xamarin.Forms as a test project last May 2023. It showcases the use of MVVM architecture with clean, extensible, and maintainable code in Xamarin.Forms.

## Problem

You have been tasked with creating a note taking application to allow the user to record short notes that can be stored and reviewed again at a later stage. The application should allow the user to perform all CRUD operations in the application on the notes. The notes are stored in the cloud so the user can access the notes across all their devices. A user should be able to favorite some notes.

**Requirements**
1. On application load, the user should see a list of existing notes. This should act as the home screen.
2. User should be able to add, read, edit and delete notes.
3. Each note should contain a title, description, date created and date last updated.
4. Date created is set on first save and never updated again.
5. Date last updated is updated on every save.
6. When a user selects a note, the note should open and the user can from here perform updates and deletions.
7. At all times the user should be able to navigate to home.
8. If the user navigates to home whilst there are unsaved changes, the user intervention should be prompted.
9. Deleted notes are deleted permenently.
10. Notes the user favourites should always appear at the top of the notes list.
11. The notes list default sort order is favourites then by date last updated descending.
12. All notes and note title are plain text.

**Additionally**
1. We would like to see small and regular commits in this repository to help us understand your approach, thought procesess and style.
2. We would like to see some basic styling applied to the application in the theme of Atlassian.
3. You can choose to create the solution in any framework and toolset that you prefer.
4. We do not expect you to create a server side project, however we expect you to mock the server APIs for performing actions on the data. Your application should use API calls to the mocks for performing persistance CRUD operations.
4. The application should make usage of asynchronous patterns as you would expect in real world conditions.
5. The list of notes in the applications should be refreshable, we do not expect push driven refreshing. Manual refreshing in app is fine.

Have fun...
