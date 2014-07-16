package com.RateIt.entity;

/**
 * Created by igor on 7/16/14.
 */
public class UserLoginQueryResult extends BaseQueryResult {

    public String UserId;
    public String SessionId;

    public String getUserId() {
        return UserId;
    }

    public void setUserId(String userId) {
        UserId = userId;
    }

    public String getSessionId() {
        return SessionId;
    }

    public void setSessionId(String sessionId) {
        SessionId = sessionId;
    }
}
