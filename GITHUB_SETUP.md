# GitHub Setup Guide for FitnessMealTrackerSolution

## Step 1: Install Git (if not already installed)

1. Download Git for Windows from: https://git-scm.com/download/win
2. Run the installer and follow the setup wizard
3. Restart your terminal/command prompt after installation

## Step 2: Initialize Git Repository

Open a terminal/command prompt in this directory and run:

```bash
cd "C:\Users\manta\Desktop\SW-Dev\FitnessMealTrackerSolution"
git init
```

## Step 3: Configure Git (if not already done)

```bash
git config --global user.name "Your Name"
git config --global user.email "your.email@example.com"
```

## Step 4: Add Files and Make Initial Commit

```bash
git add .
git commit -m "Initial commit: Fitness Meal Tracker Solution"
```

## Step 5: Create GitHub Repository

1. Go to https://github.com and sign in
2. Click the "+" icon in the top right corner
3. Select "New repository"
4. Name it: `FitnessMealTrackerSolution` (or your preferred name)
5. **DO NOT** initialize with README, .gitignore, or license (we already have these)
6. Click "Create repository"

## Step 6: Connect Local Repository to GitHub

After creating the repository, GitHub will show you commands. Use these:

```bash
git remote add origin https://github.com/YOUR_USERNAME/FitnessMealTrackerSolution.git
git branch -M main
git push -u origin main
```

Replace `YOUR_USERNAME` with your actual GitHub username.

## Alternative: Using GitHub CLI (if installed)

If you have GitHub CLI (`gh`) installed, you can create the repository directly:

```bash
gh repo create FitnessMealTrackerSolution --public --source=. --remote=origin --push
```

## Troubleshooting

- If you get authentication errors, you may need to set up a Personal Access Token:
  1. Go to GitHub Settings > Developer settings > Personal access tokens > Tokens (classic)
  2. Generate a new token with `repo` permissions
  3. Use the token as your password when pushing

- If you prefer SSH instead of HTTPS:
  ```bash
  git remote set-url origin git@github.com:YOUR_USERNAME/FitnessMealTrackerSolution.git
  ```


