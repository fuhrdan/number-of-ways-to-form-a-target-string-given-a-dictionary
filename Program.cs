//*****************************************************************************
//** 1639. Number of Ways to Form a Target String Given a Dictionaryleetcode **
//*****************************************************************************

#define MOD 1000000007

int **cnt;
int **dp;
int targetLen, wordLen;
char *target;

int dfs(int i, int j) {
    if (i >= targetLen) {
        return 1;
    }
    if (j >= wordLen) {
        return 0; 
    }
    if (dp[i][j] != -1) {
        return dp[i][j]; 
    }

    long ans = dfs(i, j + 1);
    if (cnt[j][target[i] - 'a'] > 0) {
        ans += (long) dfs(i + 1, j + 1) * cnt[j][target[i] - 'a'];
        ans %= MOD;
    }
    return dp[i][j] = (int) ans;
}

int numWays(char **words, int wordsSize, char *targetInput) {
    target = targetInput;
    targetLen = strlen(target);
    wordLen = strlen(words[0]);

    cnt = (int **)malloc(wordLen * sizeof(int *));
    for (int i = 0; i < wordLen; i++) {
        cnt[i] = (int *)calloc(26, sizeof(int));
    }

    for (int i = 0; i < wordsSize; i++) {
        for (int j = 0; j < wordLen; j++) {
            cnt[j][words[i][j] - 'a']++;
        }
    }

    dp = (int **)malloc(targetLen * sizeof(int *));
    for (int i = 0; i < targetLen; i++) {
        dp[i] = (int *)malloc(wordLen * sizeof(int));
        for (int j = 0; j < wordLen; j++) {
            dp[i][j] = -1;
        }
    }

    int result = dfs(0, 0);

    for (int i = 0; i < wordLen; i++) {
        free(cnt[i]);
    }
    free(cnt);

    for (int i = 0; i < targetLen; i++) {
        free(dp[i]);
    }
    free(dp);

    return result;
}